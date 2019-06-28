using Unity.UIWidgets.engine;
using Unity.UIWidgets.widgets;
using Unity.UIWidgets.material;
using UnityEngine;
using Unity.UIWidgets.foundation;
using System.Collections.Generic;
namespace KABBOUCHI.UIWidgets.Provider.Examples
{
    public class Example01Panel : UIWidgetsPanel
    {

        protected override Widget createWidget()
        {
            return new ProviderTest();
        }
    }

    class ProviderTest : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new MaterialApp(
             home: new Provider<int>(
                    value: 42,
                    child: new Center(
                        child: new Consumer<int>(
                        builder: (ctx, value) => new Text(value.ToString())
                      )
                )
            )
         );
        }
    }
}