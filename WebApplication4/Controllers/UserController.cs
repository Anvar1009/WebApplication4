using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using YourProject.DAL;

namespace YourProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository userRepository)
        {
            _repository = userRepository;
        }

        // Foydalanuvchilar ro'yxatini ko'rsatish
        public ActionResult Index()
        {
            var users = _repository.GetAllUsers();
            return View(users);
        }

        // Foydalanuvchi qo'shish (Get va Post)
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _repository.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Foydalanuvchini tahrirlash (Get va Post)
        [HttpGet]
        public ActionResult Edit(Int32 id)
        {
            var user = _repository.GetAllUsers().Find(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Foydalanuvchini o'chirish
        public ActionResult Delete(int id)
        {
            _repository.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
