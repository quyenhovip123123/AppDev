using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST_BE.Models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được trống")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được trống")]
        [StringLength(15)]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Điểm thưởng không được trống")]
        [DefaultValue(0)]
        public int RewardPoints { get; set; } = 0;

        [Required(ErrorMessage = "Hạng thành viên không được trống")]
        [StringLength(50)]
        [DefaultValue("Chuẩn")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
