using System;

namespace SMoni.ParsableTypes {
    public class Int32Type : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {
            return Convert.ToInt32(Value_);
        }

        public override Type getType () {
            return typeof(Int32);
        }

        #endregion

    }
}
