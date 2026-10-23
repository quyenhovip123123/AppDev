using ST_FE.Models;

namespace ST_FE.Helpers
{
    // Thanh phân trang dùng chung: [◀ Trước]  Trang x/y • n mục  [Sau ▶]
    // Gắn ngay dưới lưới dữ liệu bằng AttachBelow(); dữ liệu lấy từ API GET .../paged?page=..&pageSize=..
    public class PagerBar : FlowLayoutPanel
    {
        private readonly Button _btnPrev = new() { Text = "◀ Trước", Size = new Size(90, 30), UseVisualStyleBackColor = true };
        private readonly Label _lblPage = new() { Text = "Trang 1/1", Size = new Size(300, 30), TextAlign = ContentAlignment.MiddleCenter };
        private readonly Button _btnNext = new() { Text = "Sau ▶", Size = new Size(90, 30), UseVisualStyleBackColor = true };
        private readonly string _unit;

        public int Page { get; private set; } = 1;
        public int PageSize { get; }
        public int TotalPages { get; private set; } = 1;

        // Người dùng bấm Trước/Sau -> form tải lại dữ liệu của trang mới
        public event EventHandler? PageChanged;

        // Tham số truy vấn nối vào URL API
        public string Query => $"page={Page}&pageSize={PageSize}";

        public PagerBar(string unit, int pageSize = 15)
        {
            _unit = unit;
            PageSize = pageSize;
            Dock = DockStyle.Bottom;
            Height = 40;
            WrapContents = false;
            // Xếp nối tiếp nhau nên không bị đè khi font tự co giãn (VD chạy trên Wine thiếu Segoe UI)
            foreach (Control c in new Control[] { _btnPrev, _lblPage, _btnNext })
            {
                c.Margin = new Padding(0, 6, 6, 0);
                Controls.Add(c);
            }
            _btnPrev.Click += (_, _) => GoTo(Page - 1);
            _btnNext.Click += (_, _) => GoTo(Page + 1);
        }

        // Đặt thanh phân trang ngay dưới lưới (cùng control cha), không đè lên các panel Dock khác
        public PagerBar AttachBelow(DataGridView grid)
        {
            var parent = grid.Parent!;
            parent.Controls.Add(this);
            // Control Dock = Fill phải được sắp xếp sau cùng -> đặt thanh phân trang ngay sau lưới trong thứ tự z
            parent.Controls.SetChildIndex(this, parent.Controls.GetChildIndex(grid) + 1);
            return this;
        }

        // Về trang 1 (khi tìm kiếm / đổi bộ lọc)
        public void Reset() => Page = 1;

        // Cập nhật nhãn + nút theo kết quả. Trả về false nếu trang hiện tại vượt quá số trang
        // (VD vừa xóa dòng cuối của trang cuối) -> đã lùi về trang cuối, form cần tải lại
        public bool Apply<T>(PagedResult<T> result)
        {
            if (result.Items.Count == 0 && result.TotalPages > 0 && Page > result.TotalPages)
            {
                Page = result.TotalPages;
                return false;
            }

            TotalPages = Math.Max(1, result.TotalPages);
            _lblPage.Text = $"Trang {Page}/{TotalPages}  •  {result.TotalCount} {_unit}";
            _btnPrev.Enabled = Page > 1;
            _btnNext.Enabled = Page < TotalPages;
            return true;
        }

        private void GoTo(int page)
        {
            if (page < 1 || page > TotalPages || page == Page) return;
            Page = page;
            PageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
