using System;
using Server;

namespace Server
{
	public class ShrinkConfig
	{
		public static bool ResetBondingStatus	{ get{ return		false		;} }	//done
		public static bool TransferBondingStatus{ get{ return		false		;} }	//done
		public static bool RetainBondingTimer	{ get{ return		true		;} }	//done
		public static bool RetainSelfBondStatus	{ get{ return		true		;} }	//done
		//^^ only will work if ResetBondingStatus is false
	}
}