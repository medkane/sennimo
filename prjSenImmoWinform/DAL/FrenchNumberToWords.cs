using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjSenImmoWinform.DAL
{
    public static class FrenchNumberToWords
    {
        private static String[] dizaineNames = {
            "",
            "",
            "vingt",
            "trente",
            "quarante",
            "cinquante",
            "soixante",
            "soixante",
            "quatre-vingt",
            "quatre-vingt"
          };

        private static String[] uniteNames1 = {
            "",
            "un",
            "deux",
            "trois",
            "quatre",
            "cinq",
            "six",
            "sept",
            "huit",
            "neuf",
            "dix",
            "onze",
            "douze",
            "treize",
            "quatorze",
            "quinze",
            "seize",
            "dix-sept",
            "dix-huit",
            "dix-neuf"
          };

        private static String[] uniteNames2 = {
            "",
            "",
            "deux",
            "trois",
            "quatre",
            "cinq",
            "six",
            "sept",
            "huit",
            "neuf",
            "dix"
          };

      //  private FrenchNumberToWords() { }

        private static String convertZeroToHundred(int number)
        {

            int laDizaine = number / 10;
            int lUnite = number % 10;
            String resultat = "";

            switch (laDizaine)
            {
                case 1:
                case 7:
                case 9:
                    lUnite = lUnite + 10;
                    break;
                default:
                    break;
            }

            // s�parateur "-" "et"  ""
            String laLiaison = "";
            if (laDizaine > 1)
            {
                laLiaison = "-";
            }
            // cas particuliers
            switch (lUnite)
            {
                case 0:
                    laLiaison = "";
                    break;
                case 1:
                    if (laDizaine == 8)
                    {
                        laLiaison = "-";
                    }
                    else
                    {
                        laLiaison = " et ";
                    }
                    break;
                case 11:
                    if (laDizaine == 7)
                    {
                        laLiaison = " et ";
                    }
                    break;
                default:
                    break;
            }

            // dizaines en lettres
            switch (laDizaine)
            {
                case 0:
                    resultat = uniteNames1[lUnite];
                    break;
                case 8:
                    if (lUnite == 0)
                    {
                        resultat = dizaineNames[laDizaine];
                    }
                    else
                    {
                        resultat = dizaineNames[laDizaine]
                                                + laLiaison + uniteNames1[lUnite];
                    }
                    break;
                default:
                    resultat = dizaineNames[laDizaine]
                                            + laLiaison + uniteNames1[lUnite];
                    break;
            }
            return resultat;
        }

        private static String convertLessThanOneThousand(int number)
        {

            int lesCentaines = number / 100;
            int leReste = number % 100;
            String sReste = convertZeroToHundred(leReste);

            String resultat;
            switch (lesCentaines)
            {
                case 0:
                    resultat = sReste;
                    break;
                case 1:
                    if (leReste > 0)
                    {
                        resultat = "cent " + sReste;
                    }
                    else
                    {
                        resultat = "cent";
                    }
                    break;
                default:
                    if (leReste > 0)
                    {
                        resultat = uniteNames2[lesCentaines] + " cent " + sReste;
                    }
                    else
                    {
                        resultat = uniteNames2[lesCentaines] + " cents";
                    }
                    break;
            }
            return resultat;
        }

        public static String convert(long number)
        {
            // 0 � 999 999 999 999
            if (number == 0) { return "zéro"; }

            String snumber = number.ToString();

            // pad des "0"
            //String mask = "000000000000";
            //DecimalFormat df = new DecimalFormat(mask);
            //snumber = df.format(number);

            snumber = number.ToString().PadLeft(12, '0');
            // XXXnnnnnnnnn
            //int lesMilliards = int.Parse(snumber.Substring(0,3));

            int lesMilliards = int.Parse(snumber.Substring(0, 2));
            // nnnXXXnnnnnn
            //int lesMillions = int.Parse(snumber.Substring(3, 6));
            int lesMillions = int.Parse(snumber.Substring(3, 3));
            // nnnnnnXXXnnn
            //int lesCentMille = int.Parse(snumber.Substring(6, 9));
            int lesCentMille = int.Parse(snumber.Substring(6, 3));
            // nnnnnnnnnXXX
            //int lesMille = int.Parse(snumber.Substring(9, 12));
            int lesMille = int.Parse(snumber.Substring(9, 3));

            String tradMilliards;
            switch (lesMilliards)
            {
                case 0:
                    tradMilliards = "";
                    break;
                case 1:
                    tradMilliards = convertLessThanOneThousand(lesMilliards)
                       + " milliard ";
                    break;
                default:
                    tradMilliards = convertLessThanOneThousand(lesMilliards)
                       + " milliards ";
                    break;
            }
            String resultat = tradMilliards;

            String tradMillions;
            switch (lesMillions)
            {
                case 0:
                    tradMillions = "";
                    break;
                case 1:
                    tradMillions = convertLessThanOneThousand(lesMillions)
                       + " million ";
                    break;
                default:
                    tradMillions = convertLessThanOneThousand(lesMillions)
                       + " millions ";
                    break;
            }
            resultat = resultat + tradMillions;

            String tradCentMille;
            switch (lesCentMille)
            {
                case 0:
                    tradCentMille = "";
                    break;
                case 1:
                    tradCentMille = "mille ";
                    break;
                default:
                    tradCentMille = convertLessThanOneThousand(lesCentMille)
                       + " mille ";
                    break;
            }
            resultat = resultat + tradCentMille;

            String tradMille;
            tradMille = convertLessThanOneThousand(lesMille);
            resultat = resultat + tradMille;

            return resultat;
        }
    }
}
