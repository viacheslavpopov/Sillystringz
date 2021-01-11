using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController: Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> engineerList = _db.Engineers.ToList();
      engineerList.Sort((x, y) => string.Compare(x.LastName, y.LastName));
      return View(engineerList);
    }

    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "FirstName", "LastName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer, int MachineId)
    {
      _db.Engineers.Add(engineer);
      if (MachineId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.Machines)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
      {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult AddMachine(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
      {
        var returnedJoin = _db.EngineerMachine
          .Any(join => join.MachineId == MachineId && join.EngineerId == engineer.EngineerId);
        if (!returnedJoin)
        {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.Machines)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id, int joinId)
    {
      if (joinId != 0)
      {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.Machines)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      }
      else {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.Machines)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}