using System;
using System.Collections.Generic;
using System.Reflection;

namespace SMoni.ParsableTypes {

    public class Parser {

        /// <summary>
        /// Does exactly, what the methodname suggests.
        /// If the Type_ is unknown, a default-parser is returned.
        /// </summary>
        /// <param name="Type_">Request Parser for this type.</param>
        /// <returns>The appropriate Parser.</returns>
        public static IParsableType getParserBy(Type Type_) {

            if (Type_ == null)
                return null;

            return getParserBy(Type_.Name);

        }

        /// <summary>
        /// Does exactly, what the methodname suggests. Mind the return-value.
        /// If the TypeName_ is unknown, a default-parser is returned.
        /// </summary>
        /// <param name="TypeName_">Request Parser for this typename.</param>
        /// <returns>The appropriate Parser.</returns>
        public static IParsableType getParserBy(String TypeName_) {

            if (String.IsNullOrEmpty(TypeName_))
                return null;

            if (!Parsers.ContainsKey(TypeName_))
                createParserBy(TypeName_);

            return Parsers[TypeName_];

        }

        private static void createParserBy(String TypeName_) {

            var type = typeof(DefaultType);

            if (ParserTypes.ContainsKey(TypeName_))
                type = ParserTypes[TypeName_];

            var assembly = type.Assembly;

            var instance = assembly.CreateInstance(type.FullName,
                true,
                BindingFlags.CreateInstance,
                null,
                null,
                null,
                null);

            if (instance == null)
                throw new NullReferenceException("Instance NOT created!!! Type: " + TypeName_);

            var parser = instance as IParsableType;

            if (instance == null)
                throw new NullReferenceException("Instance does NOT implement IParsableType!!! Type: " + TypeName_);

            if (!Parsers.ContainsKey(TypeName_))
                Parsers.Add(TypeName_, parser);

        }

        private static ParsableTypes _ParserTypes;

        public static ParsableTypes ParserTypes {
            get { return _ParserTypes ?? (_ParserTypes = new ParsableTypes()); }
        }

        private static Dictionary<String, IParsableType> _Parsers;

        private static Dictionary<String, IParsableType> Parsers {
            get { return _Parsers ?? (_Parsers = new Dictionary<String, IParsableType>()); }
        }

        public class ParsableTypes : Dictionary<String, Type> {

            const String TYPE_APPENDIX = "Type";

            public ParsableTypes() {

                var executingAssembly = Assembly.GetExecutingAssembly();
                var assemblyTypes     = executingAssembly.GetTypes();

                foreach (var type in assemblyTypes) {

                    if (isTypeImplementingIParsableType(type)) {

                        var lengthOfTypeNameWithoutTrailingType = type.Name.Length - TYPE_APPENDIX.Length;
                        var nameOfTypeWithoutTrailingType       = type.Name.Substring(0, lengthOfTypeNameWithoutTrailingType);

                        this.Add(nameOfTypeWithoutTrailingType, type);

                    }

                }

            }

            private static Boolean isTypeImplementingIParsableType(Type Type_) {

                var result = false;

                if (Type_ != null)
                    result = Type_.IsClass && !Type_.IsAbstract && (Type_.GetInterface("IParsableType", true) != null);

                return result;

            }

        }

    }
}
