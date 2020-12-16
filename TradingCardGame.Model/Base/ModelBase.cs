using System;
using System.Collections.Generic;
using System.Text;

namespace TradingCardGame.Model.Base
{
    public class ModelBase : IModelBase
    {
        public int Id { get; set; }
    }

    public interface IModelBase
    {
        int Id { get; set; }
    }
}
