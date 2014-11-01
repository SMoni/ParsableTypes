using System;

namespace SMoni.ParsableTypes {
    public class BooleanType : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {

            var result = false;

            try {
            
                int valueAsInt;

                if (int.TryParse(Value_ as String, out valueAsInt))
                    result = Convert.ToBoolean(valueAsInt);
                else
                    result = Convert.ToBoolean(Value_);

            } catch (Exception) {
                //Nothing... Default is False!
            }
            
            return result;
        }

        public override Type getType () {
            return typeof(Boolean);
        }

        #endregion

    }
}
