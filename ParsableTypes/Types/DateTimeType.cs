using System;

namespace SMoni.ParsableTypes {
    public class DateTimeType : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {
            return Convert.ToDateTime(Value_);
        }

        public override Type getType () {
            return typeof(DateTime);
        }

        #endregion

    }
}
