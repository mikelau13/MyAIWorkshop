using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.Sql
{
    public class Asset
    {
        internal static List<ArticlePOCO> GetAsset(string connectionString)
        {
            List<ArticlePOCO> result = new List<ArticlePOCO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT TOP 300 Title, SubTitle, Abstract, Body
                    FROM [AR_CZuza].[z_asset].[Article] art
                    INNER JOIN [AR_CZuza].[z_asset].[Asset] ass ON art.AssetId = ass.AssetId
                    WHERE PublishFromDate > '2018-04-01'
                    AND SubTitle IS NOT NULL";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArticlePOCO each = new ArticlePOCO();
                    each.Title = dr["Title"].ToString();
                    each.SubTitle = dr["SubTitle"].ToString();
                    each.Abstract = dr["Abstract"].ToString();
                    each.Body = dr["Body"].ToString();

                    result.Add(each);
                }
                dr.Close();
                conn.Close();
            }

            return result;
        }
    }
}
