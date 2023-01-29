using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;

namespace ApplicationName.Databases.MySql.AppDatabase.DataReaders
{
    public class YahooFinanceDataReader
    {
        public List<QuoteEntityModel> ReadYahooFinanceQuoteData(string data, char delimiter)
        {
            var resultList = new List<QuoteEntityModel>();

            if (!string.IsNullOrEmpty(data))
            {
                string[] rows = data.Split("\n");

                if (rows.Length > 0)
                {
                    string[] headers = rows[0].Split(delimiter);

                    for (int i = 1; i < rows.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(rows[i]))
                        {
                            string[] values = rows[i].Split(delimiter);

                            if (headers.Length == values.Length)
                            {
                                var model = new QuoteEntityModel();

                                for (int j = 0; j < values.Length; j++)
                                {
                                    switch (headers[j])
                                    {
                                        case "Date":
                                            model.Date = Convert.ToDateTime(values[j]);
                                            break;
                                        case "Open":
                                            model.Open = Convert.ToDecimal(values[j].Replace('.', ','));
                                            break;
                                        case "High":
                                            model.High = Convert.ToDecimal(values[j].Replace('.', ','));
                                            break;
                                        case "Low":
                                            model.Low = Convert.ToDecimal(values[j].Replace('.', ','));
                                            break;
                                        case "Close":
                                            model.Close = Convert.ToDecimal(values[j].Replace('.', ','));
                                            break;
                                        case "Adj Close":
                                            model.AdjClose = Convert.ToDecimal(values[j].Replace('.', ','));
                                            break;
                                        case "Volume":
                                            model.Volume = Convert.ToInt64(values[j]);
                                            break;
                                    }
                                }

                                resultList.Add(model);
                            }
                        }
                    }
                }
            }

            return resultList;
        }
    }
}
