using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arXiv_Crawller
{
    /// <summary>
    /// 包含 arXiv 文章类别相关功能的类。
    /// </summary>
    internal class arXivCategory
    {
        /// <summary>
        /// 包含arXiv各类别及其子类别的字典。
        /// </summary>
        public static Dictionary<string, string[]> cates = new Dictionary<string, string[]> {
                                                        { "Computer Science", new string[]{ "cs.AI", "cs.AR", "cs.CC", "cs.CE", "cs.CG", "cs.CL", "cs.CR", "cs.CV", "cs.CY", "cs.DB", "cs.DC", "cs.DL", "cs.DM", "cs.DS", "cs.ET", "cs.FL", "cs.GL", "cs.GR", "cs.GT", "cs.HC", "cs.IR", "cs.IT", "cs.LG", "cs.LO", "cs.MA", "cs.MM", "cs.MS", "cs.NA", "cs.NE", "cs.NI", "cs.OH", "cs.OS", "cs.PF", "cs.PL", "cs.RO", "cs.SC", "cs.SD", "cs.SE", "cs.SI", "cs.SY" } } ,
                                                        { "Economics", new string[]{ "econ.EM", "econ.GN", "econ.TH" } } ,
                                                        { "Electrical Engineering and Systems Science", new string[]{ "eess.AS", "eess.IV", "eess.SP", "eess.SY" } } ,
                                                        { "Mathematics", new string[]{ "math.AC", "math.AG", "math.AP", "math.AT", "math.CA", "math.CO", "math.CT", "math.CV", "math.DG", "math.DS", "math.FA", "math.GM", "math.GN", "math.GT", "math.HO", "math.IT", "math.KT", "math.LO", "math.MG", "math.MP", "math.NA", "math.NT", "math.OA", "math.OC", "math.PR", "math.QA", "math.RA", "math.RT", "math.SG", "math.SP", "math.ST" } },
                                                        { "Physics", new string[]{"astro-ph", "cond-mat", "gr-qc", "hep-ex", "hep-lat", "hep-ph", "hep-th", "math-ph", "nlin", "nucl-ex", "nucl-th", "physics", "quant-ph"} },
                                                        { "Quantitative Biology", new string[]{"q-bio.BM", "q-bio.CB", "q-bio.GN", "q-bio.MN", "q-bio.NC", "q-bio.OT", "q-bio.PE", "q-bio.QM", "q-bio.SC", "q-bio.TO"}},
                                                        { "Quantitative Finance", new string[]{"q-fin.CP", "q-fin.EC", "q-fin.GN", "q-fin.MF", "q-fin.PM", "q-fin.PR", "q-fin.RM", "q-fin.ST", "q-fin.TR"}}
                                                     };



        /// <summary>
        /// 随机生成一个 arXiv 查询的类别。
        /// </summary>
        /// <returns>生成的查询类别。</returns>
        public static string RandomQuery()
        {
            Random random = new Random();

            string randomKey = cates.Keys.ElementAt(random.Next(cates.Count));

            random = new Random();

            return "cat:" + cates[randomKey][random.Next(cates[randomKey].Length)];
        }
    }
}
