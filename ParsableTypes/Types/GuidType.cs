using System;

namespace SMoni.ParsableTypes {
    public class GuidType : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {

            object result;

            try {
                result = new Guid(Value_.ToString());
            } catch (FormatException) {
                result = Guid.Empty;
            }

            return result;

        }

        public override Type getType () {
            return typeof(Guid);
        }

        #endregion

    }
}
