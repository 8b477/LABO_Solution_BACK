using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABO_Tools.Filters
{
    public class ExceptionHandlerFilter : IActionFilter
    {


        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
