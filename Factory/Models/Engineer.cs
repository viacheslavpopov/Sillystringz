using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Engineer
  {
    public Engineer()
    {
      this.Machines = new HashSet<EngineerMachine>();
    }
    public int? EngineerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; } //xxx-xxx-xxxx format
    public string Email { get; set; }
    public virtual ICollection<EngineerMachine> Machines { get; set; }
  }
}