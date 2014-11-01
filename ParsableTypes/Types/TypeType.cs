using System;

namespace SMoni.ParsableTypes {
    class TypeType : DefaultType {

        #region IType Member

        public override object parseValue (object Value_) {
            return Type.GetType((String)Value_);
        }

        public override Type getType () {
            return typeof(Type);
        }

        #endregion

    }
}
