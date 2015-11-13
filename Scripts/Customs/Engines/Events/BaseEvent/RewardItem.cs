using System;
using System.Collections.Generic;
using System.Text;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public class RewardItem
    {
        public string RewardDescription { get; set; }
        public int Amount { get; set; }
        public Type[] RewardTypeList { get; set; }

        public RewardItem(string pRewardDescription, Type[] pRewardTypeList) 
        {
            this.RewardDescription = pRewardDescription;
            this.RewardTypeList = pRewardTypeList;
            this.Amount = 1;
        }

        public RewardItem(string pRewardDescription, Type[] pRewardTypeList, int pAmount)
        {
            this.RewardDescription = pRewardDescription;
            this.RewardTypeList = pRewardTypeList;
            this.Amount = pAmount;
        }
    }
}
