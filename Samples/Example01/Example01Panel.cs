using Unity.UIWidgets.engine;
using Unity.UIWidgets.widgets;
using Unity.UIWidgets.material;
using UnityEngine;
using Unity.UIWidgets.foundation;
using System.Collections.Generic;
namespace KABBOUCHI.UIWidgets.Provider.Examples
{
    public class ModelTest : ChangeNotifier
    {
        public ModelTest()
        {
            Debug.Log("new ModelTest");
        }
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                this.notifyListeners();
            }
        }


    }

    public class Example01Panel : UIWidgetsPanel
    {

        ModelTest model = new ModelTest();

        public static Example01Panel Instance;

        protected override void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Inc();
            }

            base.Update();
        }

        public void Inc()
        {
            using (window.getScope())
            {
                model.Number++;
            }
        }

        protected override Widget createWidget()
        {
            Instance = this;
            // return new ProviderTest();
            return new ChangeNotifierProviderTest();

        }

        class ProviderTest : StatelessWidget
        {
            public override Widget build(BuildContext context)
            {
                return new MaterialApp(
                 home: new Provider<int>(
                        value: 42,
                        child: new Consumer<int>(
                            builder: (ctx, value) => new Text(value.ToString())
                        )
                    )
             );
            }
        }
        class ChangeNotifierProviderTest : StatelessWidget
        {

            public override Widget build(BuildContext context)
            {
                return new MaterialApp(
            home: new Column(
            children: new List<Widget>{
                    new RaisedButton(
                        child : new Text("Inc"),
                        onPressed: () => {
                            Example01Panel.Instance.model.Number++;
                        }
                    ),
                    new ChangeNotifierProvider<ModelTest>(
                        notifier :Example01Panel.Instance.model,
                        builder :(ctx,notifier) => new Consumer<ModelTest>(
                            builder  : (c,m) => new Text(m.Number.ToString())
                        )
                    )
                }
        )
                );
            }
        }

    }

}