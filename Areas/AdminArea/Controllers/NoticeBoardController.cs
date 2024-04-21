using EduHome.Areas.AdminArea.Views.ViewModels.Blog;
using EduHome.Areas.AdminArea.Views.ViewModels.Notice;
using EduHome.DAL;
using EduHome.Helper;
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

            var noticeBoard = _context.NoticesBoards.ToList();


            return View(noticeBoard);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(NoticeCreateVM createVM)
        {
            var board = new NoticeBoard();
            board.Description = createVM.Description;
            board.Time = createVM.Time;
            _context.NoticesBoards.Add(board);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existNotice = _context.NoticesBoards.FirstOrDefault(s => s.Id == id);
            if (existNotice == null) return NotFound();
            NoticeUpdateVM noticeUpdateVM = new NoticeUpdateVM();
            noticeUpdateVM = new NoticeUpdateVM { Time = existNotice.Time, Description = existNotice.Description };
            return View(noticeUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(NoticeUpdateVM updateVM, int? id)
        {
            if (id is null) return NotFound();
            var existNotice = _context.NoticesBoards.FirstOrDefault(s => s.Id == id);
            if (existNotice == null) return NotFound();
            existNotice.Description = updateVM.Description;
            existNotice.Time = updateVM.Time;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existNotice = _context.NoticesBoards.FirstOrDefault(s => s.Id == id);
            if (existNotice == null) return NotFound();
            return View(existNotice);
        }
        public IActionResult DeleteNotice(int? id)
        {
            if (id is null) return NotFound();
            var existNotice = _context.NoticesBoards.FirstOrDefault(s => s.Id == id);
            if (existNotice == null) return NotFound();

            _context.NoticesBoards.Remove(existNotice);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existNotice = _context.NoticesBoards.FirstOrDefault(s => s.Id == id);
            if (existNotice == null) return NotFound();
            return View(existNotice);
        }
    }
}
