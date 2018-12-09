using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class BuildingMaterialAttributes
{
    public static class Regions
    {

        public const string CHINA = "China"; // Not yet implemented
        public const string EUROPE = "Europe"; // Not yet implemented
        public const string INDIA = "India"; // Not yet implemented
        public const string ITALY = "Italy"; // Not yet implemented
        public const string NEWZEALAND = "New Zealand"; // Not yet implemented
        public const string RUSSIA = "Russia"; // Not yet implemented
        public const string SPAIN = "Spain"; // Not yet implemented
        public const string UNITEDSTATES = "United States";
        public const string VIETNAM = "Vietnam"; // Not yet implemented
        public static string[] members = { CHINA, EUROPE, INDIA, ITALY, NEWZEALAND, RUSSIA, SPAIN, UNITEDSTATES, VIETNAM };

        public static class UnitedStatesTypes
        {
            public const string STEEL = "steel";
            public const string CONCRETE = "concrete";
            public const string ALUMINUM = "aluminum";
            public const string COLDFORMED = "coldformed";
            public const string REBAR = "rebar";
            public const string TENDON = "tendon";
            public static string[] members = { STEEL, CONCRETE, ALUMINUM, COLDFORMED, REBAR, TENDON };

            public static class SteelStandards
            {
                public const string A36 = "ASTM A36";
                public const string A53 = "ASTM A53";
                public const string A500 = "ASTM A500";
                public const string A572 = "ASTM A572";
                public const string A913 = "ASTM A913";
                public const string A992 = "ASTM A992";
                public const string A709 = "ASTM A709";
                public static string[] members = { A36, A53, A500, A572, A913, A992, A709 };

                public static class A36Grades
                {
                    public const string GRADE_36 = "Grade 36";
                    public static string[] members = { GRADE_36 };
                }
                public static class A53Grades
                {
                    public const string GRADE_B = "Grade B";
                    public static string[] members = { GRADE_B };
                }
                public static class A500Grades
                {
                    public const string GRADE_B_fy_42 = "Grade B, Fy 42 (HSS Round)";
                    public const string GRADE_B_fy_46 = "Grade B, Fy 46 (HSS Rect.)";
                    public const string GRADE_C = "Grade C";
                    public static string[] members = { GRADE_B_fy_42, GRADE_B_fy_46, GRADE_C };
                }
                public static class A572Grades
                {
                    public const string GRADE_50 = "Grade 50";
                    public static string[] members = { GRADE_50 };
                }
                public static class A913Grades
                {
                    public const string GRADE_50 = "Grade 50";
                    public static string[] members = { GRADE_50 };
                }
                public static class A992Grades
                {
                    public const string GRADE_50 = "Grade 50";
                    public static string[] members = { GRADE_50 };
                }
                public static class A709Grades
                {
                    public const string GRADE_36 = "Grade 36";
                    public const string GRADE_50 = "Grade 50";
                    public const string GRADE_50S = "Grade 50S";
                    public const string GRADE_50W = "Grade 50W";
                    public const string GRADE_HPS_50W = "Grade HPS 50W";
                    public const string GRADE_HPS_70W = "Grade HPS 70W";
                    public const string GRADE_HPS_100W = "Grade HPS 100W";
                    public static string[] members = { GRADE_36, GRADE_50, GRADE_50S, GRADE_50W, GRADE_HPS_50W, GRADE_HPS_70W, GRADE_HPS_100W };
                }
            }

            public static class ConcreteStandards
            {
                public const string CUSTOMARY = "Customary";
                public static string[] members = { CUSTOMARY };

                public static class CustomaryGrades
                {
                    public const string GRADE_fc3000psi = "f'c 3000 psi";
                    public const string GRADE_fc4000psi = "f'c 4000 psi";
                    public const string GRADE_fc5000psi = "f'c 5000 psi";
                    public const string GRADE_fc6000psi = "f'c 6000 psi";
                    public const string GRADE_fc3000psi_Lightweight = "f'c 3000 psi Lightweight";
                    public const string GRADE_fc4000psi_Lightweight = "f'c 4000 psi Lightweight";
                    public const string GRADE_fc5000psi_Lightweight = "f'c 5000 psi Lightweight";
                    public const string GRADE_fc6000psi_Lightweight = "f'c 6000 psi Lightweight";
                    public static string[] members = { GRADE_fc3000psi, GRADE_fc4000psi, GRADE_fc5000psi, GRADE_fc6000psi,
                    GRADE_fc3000psi_Lightweight, GRADE_fc4000psi_Lightweight,
                    GRADE_fc5000psi_Lightweight, GRADE_fc6000psi_Lightweight };
                }
            }

            public static class AluminumStandards
            {
                public const string ASTM = "ASTM";
                public static string[] members = { ASTM };

                public static class ASTMGrades
                {
                    public const string GRADE_Alloy_6061_T6 = "Alloy 6061 T6";
                    public const string GRADE_Alloy_6063_T6 = "Alloy 6063 T6";
                    public const string GRADE_Alloy_5052_H34 = "Alloy 5052 H34";
                    public static string[] members = { GRADE_Alloy_6061_T6, GRADE_Alloy_6063_T6, GRADE_Alloy_5052_H34 };
                }
            }

            public static class ColdformedStandards
            {
                public const string ASTM_A653 = "ASTM A653";
                public static string[] members = { ASTM_A653 };

                public static class ASTM_A653Grades
                {
                    public const string GRADE_SQ_Grade_33 = "SQ Grade 33";
                    public const string GRADE_SQ_Grade_50 = "SQ Grade 50";
                    public static string[] members = { GRADE_SQ_Grade_33, GRADE_SQ_Grade_50 };
                }
            }

            public static class RebarStandards
            {
                public const string ASTM_A615 = "ASTM A615";
                public const string ASTM_A706 = "ASTM A706";
                public static string[] members = { ASTM_A615, ASTM_A706 };

                public static class ASTM_A615Grades
                {
                    public const string GRADE_40 = "Grade 40";
                    public const string GRADE_60 = "Grade 60";
                    public const string GRADE_75 = "Grade 75";
                    public static string[] members = { GRADE_40, GRADE_60, GRADE_75 };
                }
                public static class ASTM_A706Grades
                {
                    public const string GRADE_60 = "Grade 60";
                    public static string[] members = { GRADE_60 };
                }
            }

            public static class TendonStandards
            {
                public const string ASTM_A416 = "ASTM A416";
                public const string ASTM_A722 = "ASTM A722";
                public static string[] members = { ASTM_A416, ASTM_A722 };

                public static class ASTM_A416Grades
                {
                    public const string GRADE_250 = "Grade 250";
                    public const string GRADE_270 = "Grade 270";
                    public static string[] members = { GRADE_250, GRADE_270 };
                }
                public static class ASTM_A722Grades
                {
                    public const string GRADE_150_Plain_Type_1 = "Grade 150 - Plain (Type I)";
                    public const string GRADE_150_Deformed_Type_2 = "Grade 150 - Deformed (Type II)";
                    public static string[] members = { GRADE_150_Plain_Type_1, GRADE_150_Deformed_Type_2 };
                }
            }
        }

    }
}
