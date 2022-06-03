using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ibanity.Apis.Client.Utils;

namespace Ibanity.Apis.Client.Products.IsabelConnect.Models
{
    /// <summary>
    /// <p>This object provides details about an account report. From the list endpoint, you will receive a collection of the account report objects for the corresponding customer.</p>
    /// <p>Unlike other endpoints, the get endpoint will return the contents of the account report file instead of a json object. You can also find a link to the report in the account report object links.</p>
    /// </summary>
    [DataContract]
    public class AccountReport : Identified<string>
    {
        /// <summary>
        /// References of the corresponding accounts
        /// </summary>
        /// <value>References of the corresponding accounts</value>
        [DataMember(Name = "accountReferences", EmitDefaultValue = false)]
        public List<string> AccountReferences { get; set; }

        /// <summary>
        /// Format of the corresponding account report file. Possible values are &lt;code&gt;CODA&lt;/code&gt;, &lt;code&gt;MT940&lt;/code&gt;, &lt;code&gt;MT940N&lt;/code&gt;, &lt;code&gt;MT940E&lt;/code&gt;, &lt;code&gt;MT941&lt;/code&gt;, &lt;code&gt;MT942&lt;/code&gt;, &lt;code&gt;MT942N&lt;/code&gt;, &lt;code&gt;MT942E&lt;/code&gt;, &lt;code&gt;CAMT52&lt;/code&gt;, &lt;code&gt;CAMT53&lt;/code&gt;, &lt;code&gt;CAMT54&lt;/code&gt;
        /// </summary>
        /// <value>Format of the corresponding account report file. Possible values are &lt;code&gt;CODA&lt;/code&gt;, &lt;code&gt;MT940&lt;/code&gt;, &lt;code&gt;MT940N&lt;/code&gt;, &lt;code&gt;MT940E&lt;/code&gt;, &lt;code&gt;MT941&lt;/code&gt;, &lt;code&gt;MT942&lt;/code&gt;, &lt;code&gt;MT942N&lt;/code&gt;, &lt;code&gt;MT942E&lt;/code&gt;, &lt;code&gt;CAMT52&lt;/code&gt;, &lt;code&gt;CAMT53&lt;/code&gt;, &lt;code&gt;CAMT54&lt;/code&gt;</value>
        [DataMember(Name = "fileFormat", EmitDefaultValue = false)]
        public string FileFormat { get; set; }

        /// <summary>
        /// Name of the corresponding account report file
        /// </summary>
        /// <value>Name of the corresponding account report file</value>
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Size of the corresponding account report file, in bytes
        /// </summary>
        /// <value>Size of the corresponding account report file, in bytes</value>
        [DataMember(Name = "fileSize", EmitDefaultValue = false)]
        public decimal FileSize { get; set; }

        /// <summary>
        /// Name of the corresponding account&#39;s financial institution
        /// </summary>
        /// <value>Name of the corresponding account&#39;s financial institution</value>
        [DataMember(Name = "financialInstitutionName", EmitDefaultValue = false)]
        public string FinancialInstitutionName { get; set; }

        /// <summary>
        /// When the account report was received by Isabel 6. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec
        /// </summary>
        /// <value>When the account report was received by Isabel 6. Formatted according to &lt;a href&#x3D;&#39;https://en.wikipedia.org/wiki/ISO_8601&#39;&gt;ISO8601&lt;/a&gt; spec</value>
        [DataMember(Name = "receivedAt", EmitDefaultValue = false)]
        public DateTimeOffset ReceivedAt { get; set; }
    }
}
