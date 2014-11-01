using System;

namespace SMoni.ParsableTypes.Types {
    class DoubleType : DefaultType {

        #region IType Member

        public override object parseValue(object Value_) {
            return Convert.ToDouble(Value_);
        }

        public override Type getType() {
            return typeof(Double);
        }

        #endregion

    }
}
