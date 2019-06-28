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

    public class Example02Panel : UIWidgetsPanel
    {

        ModelTest model = new ModelTest();

        public static Example02Panel Instance;

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
            return new ChangeNotifierProviderTest();

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
                            Example02Panel.Instance.model.Number++;
                        }
                    ),
                    new ChangeNotifierProvider<ModelTest>(
                        notifier :Example02Panel.Instance.model,
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