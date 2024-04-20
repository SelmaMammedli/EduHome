using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class NoticeBoardController : Controller
    {
        private readonly AppDbContext _context;

        public NoticeBoardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            var noticeBoard=_context.NoticesBoards.ToList();
            
            return View(noticeBoard);
        }
    }
}
