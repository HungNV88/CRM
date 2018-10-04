using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace TamoCRM.Domain.Reports
{
    public class ReportLogDashBoardInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Tyle1 { get; set; }
        public string _Tyle1
        {
            get
            {
                if (Tyle1 == null) return "0";
                else return Tyle1;
            }
            set { }
        }

        public string Tyle2 { get; set; }
        public string _Tyle2
        {
            get
            {
                if (Tyle2 == null) return "0";
                else return Tyle2;
            }
            set { }
        }

        public string Tyle3 { get; set; }
        public string _Tyle3
        {
            get
            {
                if (Tyle3 == null) return "0";
                else return Tyle3;
            }
            set { }
        }

        public string Tyle4 { get; set; }
        public string _Tyle4
        {
            get
            {
                if (Tyle4 == null) return "0";
                else return Tyle4;
            }
            set { }
        }

        public string Tyle5 { get; set; }
        public string _Tyle5
        {
            get
            {
                if (Tyle5 == null) return "0";
                else return Tyle5;
            }
            set { }
        }

        public string Tyle6 { get; set; }
        public string _Tyle6
        {
            get
            {
                if (Tyle6 == null) return "0";
                else return Tyle6;
            }
            set { }
        }

        public string khung8_tyle1 { get; set; }
        public string _khung8_tyle1
        {
            get
            {
                if (khung8_tyle1 == null) return "0";
                else return khung8_tyle1;
            }

            set { }
        }

        public string khung8_tyle2 { get; set; }
        public string _khung8_tyle2
        {
            get
            {
                if (khung8_tyle2 == null) return "0";
                else return khung8_tyle2;
            }

            set { }
        }

        public string khung8_tyle3 { get; set; }
        public string _khung8_tyle3
        {
            get
            {
                if (khung8_tyle3 == null) return "0";
                else return khung8_tyle3;
            }

            set { }
        }

        public string khung8_tyle4 { get; set; }
        public string _khung8_tyle4
        {
            get
            {
                if (khung8_tyle4 == null) return "0";
                else return khung8_tyle4;
            }

            set { }
        }

        public string khung8_tyle5 { get; set; }
        public string _khung8_tyle5
        {
            get
            {
                if (khung8_tyle5 == null) return "0";
                else return khung8_tyle5;
            }

            set { }
        }

        public string khung8_tyle6 { get; set; }
        public string _khung8_tyle6
        {
            get
            {
                if (khung8_tyle6 == null) return "0";
                else return khung8_tyle6;
            }

            set { }
        }

        public string khung9_tyle1 { get; set; }
        public string _khung9_tyle1
        {
            get
            {
                if (khung9_tyle1 == null) return "0";
                else return khung9_tyle1;
            }

            set { }
        }

        public string khung9_tyle2 { get; set; }
        public string _khung9_tyle2
        {
            get
            {
                if (khung9_tyle2 == null) return "0";
                else return khung9_tyle2;
            }

            set { }
        }

        public string khung9_tyle3 { get; set; }
        public string _khung9_tyle3
        {
            get
            {
                if (khung9_tyle3 == null) return "0";
                else return khung9_tyle3;
            }

            set { }
        }

        public string khung9_tyle4 { get; set; }
        public string _khung9_tyle4
        {
            get
            {
                if (khung9_tyle4 == null) return "0";
                else return khung9_tyle4;
            }

            set { }
        }

        public string khung9_tyle5 { get; set; }
        public string _khung9_tyle5
        {
            get
            {
                if (khung9_tyle5 == null) return "0";
                else return khung9_tyle5;
            }

            set { }
        }

        public string khung9_tyle6 { get; set; }
        public string _khung9_tyle6
        {
            get
            {
                if (khung9_tyle6 == null) return "0";
                else return khung9_tyle6;
            }

            set { }
        }

        public string khung10_tyle1 { get; set; }
        public string _khung10_tyle1
        {
            get
            {
                if (khung10_tyle1 == null) return "0";
                else return khung10_tyle1;
            }

            set { }
        }

        public string khung10_tyle2 { get; set; }
        public string _khung10_tyle2
        {
            get
            {
                if (khung10_tyle2 == null) return "0";
                else return khung10_tyle2;
            }

            set { }
        }

        public string khung10_tyle3 { get; set; }
        public string _khung10_tyle3
        {
            get
            {
                if (khung10_tyle3 == null) return "0";
                else return khung10_tyle3;
            }

            set { }
        }

        public string khung10_tyle4 { get; set; }
        public string _khung10_tyle4
        {
            get
            {
                if (khung10_tyle4 == null) return "0";
                else return khung10_tyle4;
            }

            set { }
        }

        public string khung10_tyle5 { get; set; }
        public string _khung10_tyle5
        {
            get
            {
                if (khung10_tyle5 == null) return "0";
                else return khung10_tyle5;
            }

            set { }
        }

        public string khung10_tyle6 { get; set; }
        public string _khung10_tyle6
        {
            get
            {
                if (khung10_tyle6 == null) return "0";
                else return khung10_tyle6;
            }

            set { }
        }

        public string khung11_tyle1 { get; set; }
        public string _khung11_tyle1
        {
            get
            {
                if (khung11_tyle1 == null) return "0";
                else return khung11_tyle1;
            }

            set { }
        }

        public string khung11_tyle2 { get; set; }
        public string _khung11_tyle2
        {
            get
            {
                if (khung11_tyle2 == null) return "0";
                else return khung11_tyle2;
            }

            set { }
        }

        public string khung11_tyle3 { get; set; }
        public string _khung11_tyle3
        {
            get
            {
                if (khung11_tyle3 == null) return "0";
                else return khung11_tyle3;
            }

            set { }
        }

        public string khung11_tyle4 { get; set; }
        public string _khung11_tyle4
        {
            get
            {
                if (khung11_tyle4 == null) return "0";
                else return khung11_tyle4;
            }

            set { }
        }

        public string khung11_tyle5 { get; set; }
        public string _khung11_tyle5
        {
            get
            {
                if (khung11_tyle5 == null) return "0";
                else return khung11_tyle5;
            }

            set { }
        }

        public string khung11_tyle6 { get; set; }
        public string _khung11_tyle6
        {
            get
            {
                if (khung11_tyle6 == null) return "0";
                else return khung11_tyle6;
            }

            set { }
        }

        public string khung12_tyle1 { get; set; }
        public string _khung12_tyle1
        {
            get
            {
                if (khung12_tyle1 == null) return "0";
                else return khung12_tyle1;
            }

            set { }
        }

        public string khung12_tyle2 { get; set; }
        public string _khung12_tyle2
        {
            get
            {
                if (khung12_tyle2 == null) return "0";
                else return khung12_tyle2;
            }

            set { }
        }

        public string khung12_tyle3 { get; set; }
        public string _khung12_tyle3
        {
            get
            {
                if (khung12_tyle3 == null) return "0";
                else return khung12_tyle3;
            }

            set { }
        }

        public string khung12_tyle4 { get; set; }
        public string _khung12_tyle4
        {
            get
            {
                if (khung12_tyle4 == null) return "0";
                else return khung12_tyle4;
            }

            set { }
        }

        public string khung12_tyle5 { get; set; }
        public string _khung12_tyle5
        {
            get
            {
                if (khung12_tyle5 == null) return "0";
                else return khung12_tyle5;
            }

            set { }
        }

        public string khung12_tyle6 { get; set; }
        public string _khung12_tyle6
        {
            get
            {
                if (khung12_tyle6 == null) return "0";
                else return khung12_tyle6;
            }

            set { }
        }

        public string khung13_tyle1 { get; set; }
        public string _khung13_tyle1
        {
            get
            {
                if (khung13_tyle1 == null) return "0";
                else return khung13_tyle1;
            }

            set { }
        }

        public string khung13_tyle2 { get; set; }
        public string _khung13_tyle2
        {
            get
            {
                if (khung13_tyle2 == null) return "0";
                else return khung13_tyle2;
            }

            set { }
        }

        public string khung13_tyle3 { get; set; }
        public string _khung13_tyle3
        {
            get
            {
                if (khung13_tyle3 == null) return "0";
                else return khung13_tyle3;
            }

            set { }
        }

        public string khung13_tyle4 { get; set; }
        public string _khung13_tyle4
        {
            get
            {
                if (khung13_tyle4 == null) return "0";
                else return khung13_tyle4;
            }

            set { }
        }

        public string khung13_tyle5 { get; set; }
        public string _khung13_tyle5
        {
            get
            {
                if (khung13_tyle5 == null) return "0";
                else return khung13_tyle5;
            }

            set { }
        }

        public string khung13_tyle6 { get; set; }
        public string _khung13_tyle6
        {
            get
            {
                if (khung13_tyle6 == null) return "0";
                else return khung13_tyle6;
            }

            set { }
        }

        public string khung14_tyle1 { get; set; }
        public string _khung14_tyle1
        {
            get
            {
                if (khung14_tyle1 == null) return "0";
                else return khung14_tyle1;
            }

            set { }
        }

        public string khung14_tyle2 { get; set; }
        public string _khung14_tyle2
        {
            get
            {
                if (khung14_tyle2 == null) return "0";
                else return khung14_tyle2;
            }

            set { }
        }

        public string khung14_tyle3 { get; set; }
        public string _khung14_tyle3
        {
            get
            {
                if (khung14_tyle3 == null) return "0";
                else return khung14_tyle3;
            }

            set { }
        }

        public string khung14_tyle4 { get; set; }
        public string _khung14_tyle4
        {
            get
            {
                if (khung14_tyle4 == null) return "0";
                else return khung14_tyle4;
            }

            set { }
        }

        public string khung14_tyle5 { get; set; }
        public string _khung14_tyle5
        {
            get
            {
                if (khung14_tyle5 == null) return "0";
                else return khung14_tyle5;
            }

            set { }
        }

        public string khung14_tyle6 { get; set; }
        public string _khung14_tyle6
        {
            get
            {
                if (khung14_tyle6 == null) return "0";
                else return khung14_tyle6;
            }

            set { }
        }

        public string khung15_tyle1 { get; set; }
        public string _khung15_tyle1
        {
            get
            {
                if (khung15_tyle1 == null) return "0";
                else return khung15_tyle1;
            }

            set { }
        }

        public string khung15_tyle2 { get; set; }
        public string _khung15_tyle2
        {
            get
            {
                if (khung15_tyle2 == null) return "0";
                else return khung15_tyle2;
            }

            set { }
        }

        public string khung15_tyle3 { get; set; }
        public string _khung15_tyle3
        {
            get
            {
                if (khung15_tyle3 == null) return "0";
                else return khung15_tyle3;
            }

            set { }
        }

        public string khung15_tyle4 { get; set; }
        public string _khung15_tyle4
        {
            get
            {
                if (khung15_tyle4 == null) return "0";
                else return khung15_tyle4;
            }

            set { }
        }

        public string khung15_tyle5 { get; set; }
        public string _khung15_tyle5
        {
            get
            {
                if (khung15_tyle5 == null) return "0";
                else return khung15_tyle5;
            }

            set { }
        }

        public string khung15_tyle6 { get; set; }
        public string _khung15_tyle6
        {
            get
            {
                if (khung15_tyle6 == null) return "0";
                else return khung15_tyle6;
            }

            set { }
        }

        public string khung16_tyle1 { get; set; }
        public string _khung16_tyle1
        {
            get
            {
                if (khung16_tyle1 == null) return "0";
                else return khung16_tyle1;
            }

            set { }
        }

        public string khung16_tyle2 { get; set; }
        public string _khung16_tyle2
        {
            get
            {
                if (khung16_tyle2 == null) return "0";
                else return khung16_tyle2;
            }

            set { }
        }

        public string khung16_tyle3 { get; set; }
        public string _khung16_tyle3
        {
            get
            {
                if (khung16_tyle3 == null) return "0";
                else return khung16_tyle3;
            }

            set { }
        }

        public string khung16_tyle4 { get; set; }
        public string _khung16_tyle4
        {
            get
            {
                if (khung16_tyle4 == null) return "0";
                else return khung16_tyle4;
            }

            set { }
        }

        public string khung16_tyle5 { get; set; }
        public string _khung16_tyle5
        {
            get
            {
                if (khung16_tyle5 == null) return "0";
                else return khung16_tyle5;
            }

            set { }
        }

        public string khung16_tyle6 { get; set; }
        public string _khung16_tyle6
        {
            get
            {
                if (khung16_tyle6 == null) return "0";
                else return khung16_tyle6;
            }

            set { }
        }

        public string khung17_tyle1 { get; set; }
        public string _khung17_tyle1
        {
            get
            {
                if (khung17_tyle1 == null) return "0";
                else return khung17_tyle1;
            }

            set { }
        }

        public string khung17_tyle2 { get; set; }
        public string _khung17_tyle2
        {
            get
            {
                if (khung17_tyle2 == null) return "0";
                else return khung17_tyle2;
            }

            set { }
        }

        public string khung17_tyle3 { get; set; }
        public string _khung17_tyle3
        {
            get
            {
                if (khung17_tyle3 == null) return "0";
                else return khung17_tyle3;
            }

            set { }
        }

        public string khung17_tyle4 { get; set; }
        public string _khung17_tyle4
        {
            get
            {
                if (khung17_tyle4 == null) return "0";
                else return khung17_tyle4;
            }

            set { }
        }

        public string khung17_tyle5 { get; set; }
        public string _khung17_tyle5
        {
            get
            {
                if (khung17_tyle5 == null) return "0";
                else return khung17_tyle5;
            }

            set { }
        }

        public string khung17_tyle6 { get; set; }
        public string _khung17_tyle6
        {
            get
            {
                if (khung17_tyle6 == null) return "0";
                else return khung17_tyle6;
            }

            set { }
        }

        public string khung18_tyle1 { get; set; }
        public string _khung18_tyle1
        {
            get
            {
                if (khung18_tyle1 == null) return "0";
                else return khung18_tyle1;
            }

            set { }
        }

        public string khung18_tyle2 { get; set; }
        public string _khung18_tyle2
        {
            get
            {
                if (khung18_tyle2 == null) return "0";
                else return khung18_tyle2;
            }

            set { }
        }

        public string khung18_tyle3 { get; set; }
        public string _khung18_tyle3
        {
            get
            {
                if (khung18_tyle3 == null) return "0";
                else return khung18_tyle3;
            }

            set { }
        }

        public string khung18_tyle4 { get; set; }
        public string _khung18_tyle4
        {
            get
            {
                if (khung18_tyle4 == null) return "0";
                else return khung18_tyle4;
            }

            set { }
        }

        public string khung18_tyle5 { get; set; }
        public string _khung18_tyle5
        {
            get
            {
                if (khung18_tyle5 == null) return "0";
                else return khung18_tyle5;
            }

            set { }
        }

        public string khung18_tyle6 { get; set; }
        public string _khung18_tyle6
        {
            get
            {
                if (khung18_tyle6 == null) return "0";
                else return khung18_tyle6;
            }

            set { }
        }

        public string khung19_tyle1 { get; set; }
        public string _khung19_tyle1
        {
            get
            {
                if (khung19_tyle1 == null) return "0";
                else return khung19_tyle1;
            }

            set { }
        }

        public string khung19_tyle2 { get; set; }
        public string _khung19_tyle2
        {
            get
            {
                if (khung19_tyle2 == null) return "0";
                else return khung19_tyle2;
            }

            set { }
        }

        public string khung19_tyle3 { get; set; }
        public string _khung19_tyle3
        {
            get
            {
                if (khung19_tyle3 == null) return "0";
                else return khung19_tyle3;
            }

            set { }
        }

        public string khung19_tyle4 { get; set; }
        public string _khung19_tyle4
        {
            get
            {
                if (khung19_tyle4 == null) return "0";
                else return khung19_tyle4;
            }

            set { }
        }

        public string khung19_tyle5 { get; set; }
        public string _khung19_tyle5
        {
            get
            {
                if (khung19_tyle5 == null) return "0";
                else return khung19_tyle5;
            }

            set { }
        }

        public string khung19_tyle6 { get; set; }
        public string _khung19_tyle6
        {
            get
            {
                if (khung19_tyle6 == null) return "0";
                else return khung19_tyle6;
            }

            set { }
        }

        public string khung20_tyle1 { get; set; }
        public string _khung20_tyle1
        {
            get
            {
                if (khung20_tyle1 == null) return "0";
                else return khung20_tyle1;
            }

            set { }
        }

        public string khung20_tyle2 { get; set; }
        public string _khung20_tyle2
        {
            get
            {
                if (khung20_tyle2 == null) return "0";
                else return khung20_tyle2;
            }

            set { }
        }

        public string khung20_tyle3 { get; set; }
        public string _khung20_tyle3
        {
            get
            {
                if (khung20_tyle3 == null) return "0";
                else return khung20_tyle3;
            }

            set { }
        }

        public string khung20_tyle4 { get; set; }
        public string _khung20_tyle4
        {
            get
            {
                if (khung20_tyle4 == null) return "0";
                else return khung20_tyle4;
            }

            set { }
        }

        public string khung20_tyle5 { get; set; }
        public string _khung20_tyle5
        {
            get
            {
                if (khung20_tyle5 == null) return "0";
                else return khung20_tyle5;
            }

            set { }
        }

        public string khung20_tyle6 { get; set; }
        public string _khung20_tyle6
        {
            get
            {
                if (khung20_tyle6 == null) return "0";
                else return khung20_tyle6;
            }

            set { }
        }

        public DateTime DateTime { get; set; }

        public int Sum_tyle1
        {
            get
            {
                return int.Parse(_khung8_tyle1) + int.Parse(_khung9_tyle1) + int.Parse(_khung10_tyle1) + int.Parse(_khung11_tyle1)
                    + int.Parse(_khung12_tyle1) + int.Parse(_khung13_tyle1) + int.Parse(_khung14_tyle1) + int.Parse(_khung15_tyle1)
                    + int.Parse(_khung16_tyle1) + int.Parse(_khung17_tyle1) + int.Parse(_khung18_tyle1) + int.Parse(_khung19_tyle1)
                    + int.Parse(_khung20_tyle1);
            }
            set { }
        }

        public int Sum_tyle2
        {
            get
            {
                return int.Parse(_khung8_tyle2) + int.Parse(_khung9_tyle2) + int.Parse(_khung10_tyle2) + int.Parse(_khung11_tyle2)
                    + int.Parse(_khung12_tyle2) + int.Parse(_khung13_tyle2) + int.Parse(_khung14_tyle2) + int.Parse(_khung15_tyle2)
                    + int.Parse(_khung16_tyle2) + int.Parse(_khung17_tyle2) + int.Parse(_khung18_tyle2) + int.Parse(_khung19_tyle2)
                    + int.Parse(_khung20_tyle2);
            }
            set { }
        }

        public int Sum_tyle3
        {
            get
            {
                return int.Parse(_khung8_tyle3) + int.Parse(_khung9_tyle3) + int.Parse(_khung10_tyle3) + int.Parse(_khung11_tyle3)
                    + int.Parse(_khung12_tyle3) + int.Parse(_khung13_tyle3) + int.Parse(_khung14_tyle3) + int.Parse(_khung15_tyle3)
                    + int.Parse(_khung16_tyle3) + int.Parse(_khung17_tyle3) + int.Parse(_khung18_tyle3) + int.Parse(_khung19_tyle3)
                    + int.Parse(_khung20_tyle3);
            }
            set { }
        }

        public int Sum_tyle4
        {
            get
            {
                if (Sum_tyle3 != 0)
                    return (int)((float)Sum_tyle2 / (float)Sum_tyle3);
                else return 0;
            }
            set { }
        }

        public int SumTVTS_k8 { get; set; }
        public int SumTVTS_k9 { get; set; }
        public int SumTVTS_k10 { get; set; }
        public int SumTVTS_k11 { get; set; }
        public int SumTVTS_k12 { get; set; }
        public int SumTVTS_k13 { get; set; }
        public int SumTVTS_k14 { get; set; }
        public int SumTVTS_k15 { get; set; }
        public int SumTVTS_k16 { get; set; }
        public int SumTVTS_k17 { get; set; }
        public int SumTVTS_k18 { get; set; }
        public int SumTVTS_k19 { get; set; }
        public int SumTVTS_k20 { get; set; }

        public int SumTVTS
        {
            get
            {
                return int.Parse(_khung8_tyle2) + int.Parse(_khung9_tyle2) + int.Parse(_khung10_tyle2) + int.Parse(_khung11_tyle2)
                    + int.Parse(_khung12_tyle2) + int.Parse(_khung13_tyle2) + int.Parse(_khung14_tyle2) + int.Parse(_khung15_tyle2)
                    + int.Parse(_khung16_tyle2) + int.Parse(_khung17_tyle2) + int.Parse(_khung18_tyle2) + int.Parse(_khung19_tyle2)
                    + int.Parse(_khung20_tyle2);
            }
            set { }
        }
    }
}
