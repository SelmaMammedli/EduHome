using EduHome.Areas.AdminArea.Views.ViewModels.Board;
using EduHome.Areas.AdminArea.Views.ViewModels.Notice;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BoardController : Controller
    {
        private readonly AppDbContext _context;

        public BoardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var datas=_context.Boards.ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(BoardCreateVM createVM)
        {
            var board = new Board();
            board.Description = createVM.Description;
            board.Title = createVM.Title;
            _context.Boards.Add(board);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();
            var existBoard = _context.Boards.FirstOrDefault(s => s.Id == id);
            if (existBoard == null) return NotFound();
            BoardUpdateVM boardUpdateVM = new BoardUpdateVM();
            boardUpdateVM = new BoardUpdateVM { Title = existBoard.Title, Description = existBoard.Description };
            return View(boardUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(BoardUpdateVM updateVM,int? id)
        {
            if (id is null) return NotFound();
            var existBoard = _context.Boards.FirstOrDefault(s => s.Id == id);
            if (existBoard == null) return NotFound();
            existBoard.Title = updateVM.Title;
            existBoard.Description = updateVM.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var existBoard = _context.Boards.FirstOrDefault(s => s.Id == id);
            if (existBoard == null) return NotFound();
            return View(existBoard);
        }
        public IActionResult DeleteBoard(int? id)
        {
            if (id is null) return NotFound();
            var existBoard = _context.Boards.FirstOrDefault(s => s.Id == id);
            if (existBoard == null) return NotFound();

            _context.Boards.Remove(existBoard);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();
            var existBoard = _context.Boards.FirstOrDefault(s => s.Id == id);
            if (existBoard == null) return NotFound();
            return View(existBoard);
        }
    }
}
