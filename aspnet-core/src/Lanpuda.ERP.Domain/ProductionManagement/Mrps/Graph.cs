using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Mrps
{
    public class Graph
    {
        private MrpNode[] mrpNodes;
        private int nodenum;

        public Graph(MrpNode[] MrpNodes)
        {
            //取得node的数量
            for (int i = 0; i < MrpNodes.Length; i++)
            {
                if (MrpNodes[i] != null)
                {
                    nodenum++;
                }
            }
            //复制数组
            mrpNodes = new MrpNode[MrpNodes.Length];
            for (int i = 0; i < MrpNodes.Length; i++)
            {
                mrpNodes[i] = MrpNodes[i];
            }
        }


        public MrpNode[] List()
        {

            Stack<MrpNode> stack = new Stack<MrpNode>();//栈

            for (int i = 0; i < this.nodenum; i++)
            {

                if (mrpNodes[i].Indegree == 0)
                { //根据拓扑排序算法，只有入度为0才开始运算
                    stack.Push(mrpNodes[i]);
                }
            }
            while (stack.Count != 0)
            {
                MrpNode node = (MrpNode)stack.Pop();

                //遍历毛需求并进行运算
                foreach (var order in node.Orders)
                {
                    if (order.Value != 0)
                    {
                        //判断：如果当前库存够用，就从当前库存中减去订单中的数量
                        if ((order.Value) - node.OnHand <= 0)
                        {
                            node.OnHand = node.OnHand - order.Value;
                        }
                        //如果库存不足，则计算需要多少个
                        else
                        {
                            if (node.WorkOrders.ContainsKey(order.Key.AddDays(-node.LeadTime)))
                            {
                                node.WorkOrders[order.Key.AddDays(-node.LeadTime)] = order.Value - node.OnHand;
                            }
                            else
                            {
                                node.WorkOrders.Add(order.Key.AddDays(-node.LeadTime), order.Value - node.OnHand);
                            }
                            node.OnHand = 0;
                        }
                    }
                }

               

                for (MrpLine p = node.FirstMrpLine; p != null; p = p.NextMrpLine)
                {
                    MrpNode tarNode = p.TarMrpNode;

                    foreach (var item in node.WorkOrders)
                    {
                        if (tarNode.Orders.ContainsKey(item.Key))
                        {
                            tarNode.Orders[item.Key] = tarNode.Orders[item.Key] + (item.Value * p.NeedQuantity);
                        }
                        else
                        {
                            tarNode.Orders.Add(item.Key, item.Value * p.NeedQuantity)  ;
                        }
                        
                    }

                    tarNode.Indegree--;
                    if (tarNode.Indegree == 0)
                    {
                        stack.Push(tarNode);
                    }
                }

            }
            return mrpNodes;
        }
    }
}
