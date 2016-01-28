using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Unity
{
    public class WebPartUnityContainer
    {
        public IUnityContainer Container
        {
            get;
            private set;
        }

        private static WebPartUnityContainer _current;
        public static WebPartUnityContainer Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new WebPartUnityContainer();
                }

                return _current;
            }
        }

        private WebPartUnityContainer()
        {
            Container = new UnityContainer();

            SharePointInstanceProvider.RegisterTypes(Container);
        }
    }
}
