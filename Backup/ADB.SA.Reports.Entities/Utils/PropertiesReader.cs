using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Entities.Utils
{
    public class PropertiesReader
    {
        public static List<PropertiesDTO> ReadShortProperties(string properties)
        {
            List<PropertiesDTO> props = new List<PropertiesDTO>();

            string[] splited = properties.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i <= splited.Length; i++)
            {
                if ((i + 1) >= splited.Length)
                {
                    return props;
                }

                if (splited[i].Contains("[[[") && splited[i].Contains("]]]"))
                {
                    PropertiesDTO dto = new PropertiesDTO();

                    //dto.Key = splited[i].Replace("[[[", string.Empty).Replace("]]]", string.Empty);
                    dto.Key = splited[i].Substring(3, splited[i].Length - 6);


                    if (PeekNextInArray(splited, i).Contains("[[[")
                        && PeekNextInArray(splited, i).Contains("]]]"))
                    {
                        continue;
                    }
                    else
                    {
                        int add = AddNextMultipleValues(dto, splited, i);
                        i = add - 1;
                    }
                    props.Add(dto);
                }

            }

            return props;
        }

        private static int AddNextMultipleValues(PropertiesDTO dto, string[] arr,
                                            int currentIndex)
        {
            bool toContinue = true;
            string peekedValue;
            while (toContinue)
            {
                peekedValue = PeekNextInArray(arr, currentIndex);
                if ((peekedValue.Contains("[[[") && peekedValue.Contains("]]]"))
                    || (arr.Length == (currentIndex + 1)))
                {
                    toContinue = false;
                }
                else
                {
                    dto.Value.Add(peekedValue.Replace("\"", string.Empty));
                    currentIndex++;
                }
            }
            return currentIndex;
        }

        private static string PeekNextInArray(string[] arr, int index)
        {
            if (arr.Length == (index + 1))
            {
                return string.Empty;
            }
            return arr[index + 1];
        }
    }
}
