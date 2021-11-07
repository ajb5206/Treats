using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierreTreats.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierreTreats.Controllers
{
	[Authorize]
	public class FlavorsController : Controller
	{
		
		private readonly PierreTreatsContext _db;
		private readonly UserManager <ApplicationUser> _userManager;
		public FlavorsController(UserManager <ApplicationUser> userManager, PierreTreatsContext db)
		{
			_userManager = userManager;
			_db = db;
		}
		[AllowAnonymous]
		public ActionResult Index(string searchString)
		{
			IQueryable<Flavor> model = _db.Flavors;
			if(!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(model => model.FlavorName.Contains(searchString));
			}
			return View(model.ToList());
		}

		public ActionResult Create()
		{
			ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
			return View();
		}

		[HttpPost]
		public ActionResult Create(Flavor flavor, int TreatId)
		{
			_db.Flavors.Add(flavor);
			_db.SaveChanges();
			if (TreatId !=0 )
			{
				_db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId});
			}
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
			return View(thisFlavor);
		}

		[HttpPost]
		public ActionResult Edit(Flavor flavor, int TreatId)
		{
			_db.Entry(flavor).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			var thisFlavor = _db.Flavors
				.Include(flavor => flavor.JoinEntities)
				.ThenInclude(join => join.Treat)
				.FirstOrDefault(flavor => flavor.FlavorId ==id);
			return View(thisFlavor);
		}

		public ActionResult AddTreat(int id)
		{
			var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
			ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
			return View(thisFlavor);
		}

		[HttpPost]
		public ActionResult AddTreat(Flavor flavor, int TreatId)
		{	
			if (TreatId != 0)
			{
				if (_db.FlavorTreat.Any(x => x.TreatId == TreatId && x.FlavorId == flavor.FlavorId) == false)
			{
				_db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId});
			}
			}
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId ==id );
			return View(thisFlavor);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var thisFlavor = _db.Flavors.FirstOrDefault( flavor => flavor.FlavorId == id );
			_db.Flavors.Remove(thisFlavor);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult DeleteTreat(int joinId)
		{
			var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId );
			_db.FlavorTreat.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}