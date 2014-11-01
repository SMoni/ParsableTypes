using System;

namespace SMoni.ParsableTypes {
    public interface IParsableType {
        object parseValue (object Value_);
        T      parseValue<T>(object Value_);

        Type getType ();
    }
}
