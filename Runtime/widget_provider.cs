using System;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace KABBOUCHI.UIWidgets.Provider
{
    public class Provider
    {

        static Type _typeOf<T>()
        {
            return typeof(T);
        }
        public static T of<T>(BuildContext context, bool listen = true)
        {
            var type = _typeOf<Provider<T>>();

            Provider<T> provider = listen ? context.inheritFromWidgetOfExactType(type) as Provider<T> :
            context.ancestorInheritedElementForWidgetOfExactType(type)?.widget as Provider<T>;

            if (provider == null)
            {
                throw new UIWidgetsError("Provider<T> is missing");
            }

            return provider.value;
        }
    }

    public class Provider<T> : InheritedWidget
    {
        public readonly T value;
        readonly Func<T, T, bool> _updateShouldNotify;

        public Provider(T value, Widget child, Func<T, T, bool> updateShouldNotify = null, Key key = null) : base(key, child)
        {
            this.value = value;
            _updateShouldNotify = updateShouldNotify;
        }

        public Provider<T> cloneWithChild(Widget child)
        {
            return new Provider<T>(
                    key: key,
                    value: value,
                    child: child,
                    updateShouldNotify: _updateShouldNotify
         );
        }

        public override bool updateShouldNotify(InheritedWidget oldWidget)
        {
            T oldValue = ((Provider<T>)oldWidget).value;

            if (_updateShouldNotify != null)
            {
                return _updateShouldNotify(oldValue, this.value);
            }

            return !Equals(this.value, oldValue);
        }


    }

    public class StatefulProvider<T> : StatefulWidget
    {
        public readonly Func<BuildContext, T> valueBuilder;
        public readonly Action<BuildContext, T> onDispose;
        public readonly Widget child;
        public readonly Func<T, T, bool> updateShouldNotify;

        public StatefulProvider(Func<BuildContext, T> valueBuilder, Widget child, Func<T, T, bool> updateShouldNotify = null, Key key = null) : base(key)
        {
            this.valueBuilder = valueBuilder;
            this.child = child;
            this.updateShouldNotify = updateShouldNotify;
        }

        public override State createState()
        {
            return new _StatefulProviderState<T>();
        }
    }

    class _StatefulProviderState<T> : State<StatefulProvider<T>>
    {
        T _value;

        public override void initState()
        {
            base.initState();
            _value = widget.valueBuilder(context);
        }

        public override void dispose()
        {
            if (widget.onDispose != null)
            {
                widget.onDispose(context, _value);
            }
            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return new Provider<T>(
              value: _value,
              updateShouldNotify: widget.updateShouldNotify,
              child: widget.child
            );
        }
    }
}