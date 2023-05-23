using System;

public class Class1
{
	public Class1()
	{
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
