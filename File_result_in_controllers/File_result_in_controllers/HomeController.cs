using Microsoft.AspNetCore.Mvc;

namespace File_result_in_controllers
{
    [Controller]
    public class HomeController: Controller
    {
        [Route("file-download")]

        public VirtualFileResult Filedownload()
        {
            return File("/dummypdf.pdf", "application/pdf");
        }

        [Route("file-download2")]

        public PhysicalFileResult Filedownload2()
        {
            //return new PhysicalFileResult("C:\\Users\\TanujTewatia\\Desktop\\dummypdf.pdf", "application/pdf");
            return PhysicalFile("C:\\Users\\TanujTewatia\\Desktop\\dummypdf.pdf", "application/pdf");
        }
    }
}
