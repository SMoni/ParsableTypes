using System;
//
using Xunit;

namespace SMoni.ParsableTypes.Test {

    public class Suite {

        [Fact]
        public void parseDefaultType() {

            var parsableType = Parser.getParserBy(typeof(Object));

            var expectedValue = "Hallo";

            var actualValue = parsableType.parseValue(expectedValue);

            Assert.Equal(expectedValue, actualValue);

                            
        }

        [Fact]
        public void parseBoolean() {

            var parsableType = Parser.getParserBy(typeof(Boolean));

            var expectedValueAsString = "true";

            var expectedValue = true;

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseDateTime() {

            var parsableType = Parser.getParserBy(typeof(DateTime));

            var expectedValueAsString = "1.1.2010 12:00:00";

            var expectedValue = DateTime.Parse(expectedValueAsString);

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseDecimal() {

            var parsableType = Parser.getParserBy(typeof(Decimal));

            var expectedValueAsString = "10,0";

            var expectedValue = 10.0m;

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseDouble() {

            var parsableType = Parser.getParserBy(typeof(Double));

            var expectedValueAsString = "10,0";

            var expectedValue = 10.0;

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseGuid() {

            var parsableType = Parser.getParserBy(typeof(Guid));

            var expectedValueAsString = Guid.Empty.ToString();

            var expectedValue = Guid.Empty;

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseInt32() {

            var parsableType = Parser.getParserBy(typeof(Int32));

            var expectedValueAsString = "42";

            var expectedValue = 42;

            var actualValue = parsableType.parseValue<Int32>(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void parseType() {

            var parsableType = Parser.getParserBy(typeof(Type));

            var expectedValueAsString = "System.String";

            var expectedValue = typeof(String);

            var actualValue = parsableType.parseValue(expectedValueAsString);

            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public void useCustomParser() {

            var typeOfCustomClass = typeof(CustomStruct);

            Parser.ParserTypes.Add(typeOfCustomClass.Name, typeof(CustomType));

            var customClassParser = Parser.getParserBy(typeOfCustomClass);

            Assert.NotNull(customClassParser);

            Assert.Equal(typeOfCustomClass, customClassParser.getType());

            var parsedObject = customClassParser.parseValue<CustomStruct>("Hello;2014-01-01");

            Assert.NotNull(parsedObject);

            Assert.Equal("Hello",                      parsedObject.Value);
            Assert.Equal(DateTime.Parse("2014-01-01"), parsedObject.From);

        }

        public struct CustomStruct {
            public String   Value { get; set; }
            public DateTime From  { get; set; }
        }

        public class CustomType : DefaultType {

            #region IType Member

            public override object parseValue(object Value_) {

                if (!(Value_ is String))
                    throw new ArgumentException("Value NOT String!!!", "Value_");

                var typeName = getType().FullName;
                var assembly = getType().Assembly;

                var result = assembly.CreateInstance(typeName);

                if (result == null)
                    throw new NullReferenceException("CustomClass NOT created!!!");

                var resultAsCustomStruct = (CustomStruct)result;

                var values = (Value_ as String).Split(new[] { ';' });

                resultAsCustomStruct.Value = values[0];
                resultAsCustomStruct.From = DateTime.Parse(values[1]);

                return resultAsCustomStruct;
            }

            public override Type getType() {
                return typeof(CustomStruct);
            }

            #endregion

        }

    }
}
