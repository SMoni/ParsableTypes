using System;

namespace SMoni.ParsableTypes {
    public class DecimalType : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {
            return Convert.ToDecimal(Value_);
        }

        public override Type getType () {
            return typeof(Decimal);
        }

        #endregion

    }
}
