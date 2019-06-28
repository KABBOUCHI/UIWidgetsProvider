using System;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace KABBOUCHI.UIWidgets.Provider
{

    public class ChangeNotifierProvider<T> : StatefulWidget where T : ChangeNotifier
    {
        public readonly T notifier;
        public readonly Func<BuildContext, T, Widget> builder;
        public readonly Widget child;

        public ChangeNotifierProvider(T notifier, Func<BuildContext, T, Widget> builder, Key key = null) : base(key)
        {
            this.notifier = notifier;
            this.builder = builder;
        }

        public override State createState()
        {
            return new _ChangeNotifierProviderState<T>();
        }
    }

    class _ChangeNotifierProviderState<T> : State<ChangeNotifierProvider<T>> where T : ChangeNotifier
    {
        public override void initState()
        {
            base.initState();

            widget.notifier.addListener(rebuild);
        }

        public override void dispose()
        {
            base.dispose();

            widget.notifier.removeListener(rebuild);
        }

        void rebuild()
        {
            setState();
        }


        public override Widget build(BuildContext context)
        {
            return new Provider<T>(
                value: widget.notifier,
                child: widget.builder(context, widget.notifier)
            );
        }
    }

}