using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (this.Price < 100)
            //{
            //    yield return new ValidationResult("價格過低, 設定錯誤 !", new string[] { "Price" });
            //}

            if (this.Stock > 2000)
            {
                yield return new ValidationResult("庫存過高, 設定錯誤 !", new string[] { "Stock" });
            }

            
        }
    }

    public class ProductMetadata
    {
        /// <summary>
        /// 商品 Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        //[TwoSpaceRule(ErrorMessage = "Please input two space char !")]
        public string ProductName { get; set; }

        /// <summary>
        /// 價格
        /// </summary>
        public Nullable<decimal> Price { get; set; }

        /// <summary>
        /// 是否作用
        /// </summary>
        public Nullable<bool> Active { get; set; }
        
        /// <summary>
        /// 庫存量
        /// </summary>
        public Nullable<decimal> Stock { get; set; }


        [JsonIgnore]
        public virtual ICollection<OrderLine> OrderLine { get; set; }

    }
}