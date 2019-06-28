using System;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.widgets;

namespace KABBOUCHI.UIWidgets.Provider
{
    public class Consumer<T> : StatelessWidget
    {
        Func<BuildContext, T, Widget> builder;

        public Consumer(Func<BuildContext, T, Widget> builder, Key key = null) : base(key)
        {
            this.builder = builder;
        }

        public override Widget build(BuildContext context)
        {
            return builder(context, Provider.of<T>(context));
        }
    }

    public class Consumer<T1, T2> : StatelessWidget
    {
        Func<BuildContext, T1, T2, Widget> builder;

        public Consumer(Func<BuildContext, T1, T2, Widget> builder, Key key = null) : base(key)
        {
            this.builder = builder;
        }

        public override Widget build(BuildContext context)
        {
            return builder(context, Provider.of<T1>(context), Provider.of<T2>(context));
        }
    }

}