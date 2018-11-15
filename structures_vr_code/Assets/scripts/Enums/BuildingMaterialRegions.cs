using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingMaterialRegions {
    public const int CHINA = 10000; // Not yet implemented
    public const int EUROPE = 20000; // Not yet implemented
    public const int INDIA = 30000; // Not yet implemented
    public const int ITALY = 40000; // Not yet implemented
    public const int NEWZEALAND = 50000; // Not yet implemented
    public const int RUSSIA = 60000; // Not yet implemented
    public const int SPAIN = 70000; // Not yet implemented
    public const int UNITEDSTATES = 80000;
    public const int VIETNAM = 90000; // Not yet implemented

    public static class UnitedStatesTypes
    {
        public const int STEEL = 81000;
        public const int CONCRETE = 82000;
        public const int ALUMINUM = 83000;
        public const int COLDFORMED = 84000;
        public const int REBAR = 85000;
        public const int TENDON = 85000;

        public static class SteelStandards
        {
            public const int A36 = 81100;
            public const int A53 = 81200;
            public const int A500 = 81300;
            public const int A572 = 81400;
            public const int A913 = 81500;
            public const int A992 = 81600;
            public const int A702 = 81700;

            public static class A36Grades
            {
                public const int GRADE_36 = 81110;
            }
            public static class A53Grades
            {
                public const int GRADE_B = 81210;
            }
            public static class A500Grades
            {
                public const int GRADE_B_fy_42 = 81310;
                public const int GRADE_B_fy_46 = 81320;
                public const int GRADE_C = 81330;
            }
            public static class A572Grades
            {
                public const int GRADE_50 = 81410;
            }
            public static class A913Grades
            {
                public const int GRADE_50 = 81510;
            }
            public static class A992Grades
            {
                public const int GRADE_50 = 81610;
            }
            public static class A709Grades
            {
                public const int GRADE_36 = 81610;
                public const int GRADE_50 = 81620;
                public const int GRADE_50S = 81630;
                public const int GRADE_50W = 81640;
                public const int GRADE_HPS_50W = 81650;
                public const int GRADE_HPS_70W = 81660;
                public const int GRADE_HPS_100W = 81670;
            }
        }

        public static class ConcreteStandards
        {
            public const int CUSTOMARY = 82100;

            public static class CustomaryGrades
            {
                public const int GRADE_fc3000psi = 82110;
                public const int GRADE_fc4000psi = 82120;
                public const int GRADE_fc5000psi = 82130;
                public const int GRADE_fc6000psi = 82140;
                public const int GRADE_fc3000psi_Lightweight = 82150;
                public const int GRADE_fc4000psi_Lightweight = 82160;
                public const int GRADE_fc5000psi_Lightweight = 82170;
                public const int GRADE_fc6000psi_Lightweight = 82180;
            }
        }

        public static class AluminumStandards
        {
            public const int ASTM = 83100;

            public static class ASTMGrades
            {
                public const int GRADE_Alloy_6061_T6 = 83110;
                public const int GRADE_Alloy_6063_T6 = 83120;
                public const int GRADE_Alloy_5052_H34 = 83130;
            }
        }

        public static class ColdformedStandards
        {
            public const int ASTM_A653 = 84100;

            public static class ASTM_A653Grades
            {
                public const int GRADE_SQ_Grade_33 = 84110;
                public const int GRADE_SQ_Grade_50 = 84120;
            }
        }

        public static class RebarStandards
        {
            public const int ASTM_A615 = 85100;
            public const int ASTM_A706 = 85200;

            public static class ASTM_A615Grades
            {
                public const int GRADE_40 = 85110;
                public const int GRADE_60 = 85120;
                public const int GRADE_75 = 85130;
            }
            public static class ASTM_A706Grades
            {
                public const int GRADE_60 = 85210;
            }
        }

        public static class TendonStandards
        {
            public const int ASTM_A416 = 86100;
            public const int ASTM_A722 = 86200;

            public static class ASTM_A416Grades
            {
                public const int GRADE_250 = 86110;
                public const int GRADE_270 = 86120;
            }
            public static class ASTM_A722Grades
            {
                public const int GRADE_150_Plain_Type_1 = 86210;
                public const int GRADE_150_Deformed_Type_2 = 86220;
            }
        }
    }

}
