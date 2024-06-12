using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Mrps
{
    public class MrpLine
    {
        public MrpNode CurrMrpNode { get; set; }
        public MrpNode TarMrpNode { get; set; }

        /// <summary>
        /// 组成数量
        /// </summary>
        public double NeedQuantity { get; set; }

        public MrpLine NextMrpLine { get; set; }

        public MrpLine(MrpNode crrentNode, MrpNode tarNode, double needQuantity)
        {
            CurrMrpNode = crrentNode;
            TarMrpNode = tarNode;
            NeedQuantity = needQuantity;
            NextMrpLine = null;
            if (crrentNode.FirstMrpLine == null)
            {
                CurrMrpNode.FirstMrpLine = this;
                CurrMrpNode.CurrMrpLine = this;
            }
            else
            {
                CurrMrpNode.CurrMrpLine.NextMrpLine = this;
                CurrMrpNode.CurrMrpLine = this;
            }
            TarMrpNode.Indegree++;
        }
    }
}
