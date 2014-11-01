using System;

namespace SMoni.ParsableTypes {
    public class DefaultType : IParsableType {

        #region IType Member

        public virtual object parseValue (object Value_) {
            return Value_;
        }

        public virtual T parseValue<T>(object Value_) {
            return (T)parseValue(Value_);
        }

        public virtual Type getType () {
            return typeof(ValueType);
        }

        #endregion

    }
}
