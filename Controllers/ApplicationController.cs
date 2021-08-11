using Finance_task2.Data;
using Finance_task2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_task2.Controllers
{
    public class ApplicationController : Controller
    {
        //call DbContext as property
        private readonly ApplicationDbContext _db;


        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }

        //region users list
        public IActionResult Index()
        {
            var users = (from u in _db.Users
                         join r in _db.Regions on u.RegionId equals r.id
                         select new
                         {
                             Id = u.id,
                             Name = u.Name,
                             Mahalla = u.Mahalla,
                             District = u.DistrictId,
                             Region = r.Name
                         }).ToList().Select(x => new Users()
                         {
                             id = x.Id,
                             Name = x.Name,
                             Mahalla = x.Mahalla,
                             RegionName = x.Region,
                             DistrictId = x.District
                         });

            users=
                (from u in _db.Users
                 join d in _db.Districts
                           on u.DistrictId equals d.id
                 select new
                 {
                     Id = u.id,
                     Name = u.Name,
                     Mahalla = u.Mahalla,
                     District = d.Name,
                     Region = u.RegionName
                 }).ToList().Select(x => new Users()
                 {
                     id = x.Id,
                     Name = x.Name,
                     Mahalla = x.Mahalla,
                     RegionName = x.Region,
                     DistrictName = x.District
                 });
            /**/
            return View(users.ToList());
        }
        //region users list sorting and searching
        [HttpGet]
        public async Task<IActionResult> Index(string userSearch, string sortingUser)
        {
            var userQuery = from x in _db.Users select x;
            userQuery = userQuery.OrderBy(x => x.Name);

            ViewData["GetUsersDetails"] = userSearch;

            ViewData["id"] = string.IsNullOrEmpty(sortingUser) ? "id" : "";

            switch (sortingUser)
            {
                case "id":
                    userQuery = userQuery.OrderBy(x => x.id);
                    break;

            }

            ViewData["Name"] = string.IsNullOrEmpty(sortingUser) ? "Name" : "";

            switch (sortingUser)
            {
                case "Name":
                    userQuery = userQuery.OrderBy(x => x.Name);
                    break;

            }

            ViewData["Region"] = string.IsNullOrEmpty(sortingUser) ? "Region" : "";

            switch (sortingUser)
            {
                case "Region":
                    userQuery = userQuery.OrderBy(x => x.Region.Name);
                    break;

            }

            ViewData["District"] = string.IsNullOrEmpty(sortingUser) ? "District" : "";

            switch (sortingUser)
            {
                case "District":
                    userQuery = userQuery.OrderBy(x => x.District.Name);
                    break;

            }

            ViewData["Mahalla"] = string.IsNullOrEmpty(sortingUser) ? "Mahalla" : "";

            switch (sortingUser)
            {
                case "Mahalla":
                    userQuery = userQuery.OrderBy(x => x.Mahalla);
                    break;

            }

            if (!String.IsNullOrEmpty(userSearch))
            {
                userQuery = userQuery.Where(x => x.id.Equals(userSearch) ||
                x.Name.Contains(userSearch) || x.Region.Name.Contains(userSearch) ||
                x.District.Name.Contains(userSearch) || x.Mahalla.Contains(userSearch));
            }
            return View(await userQuery.AsNoTracking().ToListAsync());
        }
        //region calling of users creating form 
        public IActionResult Create()
        {
            var getRegion = _db.Regions.ToList();

            SelectList list = new SelectList(getRegion, "id", "Name");
            ViewBag.getregionName = list;

            var getDistrict = _db.Districts.ToList();
            SelectList list2 = new SelectList(getDistrict, "id", "Name");
            ViewBag.getDistrictName = list2;

            return View();
        }
        //region users creating and  saving database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users obj)
        {
            _db.Users.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // region calling of delete form
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        //delete user and save results into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        //calking of update form
        public IActionResult Update(int? id)
        {
            var getRegion = _db.Regions.ToList();

            SelectList list = new SelectList(getRegion, "id", "Name");
            ViewBag.getregionName = list;

            var getDistrict = _db.Districts.ToList();
            SelectList list2 = new SelectList(getDistrict, "id", "Name");
            ViewBag.getDistrictName = list2;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        //saving changes into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Users obj)
        {
            if (ModelState.IsValid)
            {
                var data = _db.Users.FirstOrDefault(x => x.id == id);

                if (data != null)
                {
                    _db.Entry(data).CurrentValues.SetValues(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");

                }
               
            }
            return View(obj);

        }
    }
}
