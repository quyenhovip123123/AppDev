using System.Globalization;
using System.Text;

namespace ST_FE.Helpers
{
    // Xuất dữ liệu ra tệp .csv (UTF-8 có BOM để Excel hiển thị đúng tiếng Việt)
    public static class CsvExporter
    {
        public record Column<T>(string Header, Func<T, object?> Value);

        // Hỏi nơi lưu rồi ghi tệp. Trả về false nếu người dùng bấm Hủy
        public static bool SaveWithDialog<T>(IWin32Window owner, string defaultFileName, IEnumerable<T> rows, params Column<T>[] columns)
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Xuất dữ liệu ra Excel (CSV)",
                Filter = "Tệp CSV mở bằng Excel (*.csv)|*.csv",
                FileName = defaultFileName,
                DefaultExt = "csv",
                AddExtension = true
            };
            if (dialog.ShowDialog(owner) != DialogResult.OK) return false;

            var sb = new StringBuilder();
            sb.AppendLine(string.Join(",", columns.Select(c => Escape(c.Header))));
            foreach (var row in rows)
                sb.AppendLine(string.Join(",", columns.Select(c => Escape(Format(c.Value(row))))));

            File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            return true;
        }

        private static string Format(object? value) => value switch
        {
            null => string.Empty,
            DateTime d => d.ToString("dd/MM/yyyy HH:mm"),
            bool b => b ? "Có" : "Không",
            IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),   // số không có dấu phân cách hàng nghìn
            _ => value.ToString() ?? string.Empty
        };

        // Bọc trong dấu nháy kép nếu có dấu phẩy, nháy kép hoặc xuống dòng
        private static string Escape(string s) =>
            s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0 ? "\"" + s.Replace("\"", "\"\"") + "\"" : s;
    }
}
