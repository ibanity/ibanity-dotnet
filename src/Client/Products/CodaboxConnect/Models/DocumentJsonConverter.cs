using System;
using Ibanity.Apis.Client.JsonApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ibanity.Apis.Client.Products.CodaboxConnect.Models
{
    /// <summary>
    /// Polymorphic documents converter
    /// </summary>
    public class DocumentJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType) =>
            typeof(Data<IDocument, object, DocumentRelationships, object>).IsAssignableFrom(objectType);

        /// <inheritdoc />
        public override bool CanRead => true;

        /// <inheritdoc />
        public override bool CanWrite => false;

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            var type = item["type"].Value<string>();

            switch (type)
            {
                case "creditCardStatement": return item.ToObject<DocumentData<CreditCardStatement>>();
                case "purchaseInvoice": return item.ToObject<DocumentData<PurchaseInvoice>>();
                case "salesInvoice": return item.ToObject<DocumentData<SalesInvoice>>();
                case "payrollStatement": return item.ToObject<DocumentData<PayrollStatement>>();
                case "bankAccountStatement": return item.ToObject<DocumentData<BankAccountStatement>>();
                default: return item.ToObject<Document<string>>();
            }
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();
    }
}
