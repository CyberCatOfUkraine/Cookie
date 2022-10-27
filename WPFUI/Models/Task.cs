﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class Task
    {
        public Task(DateTime started, DateTime finished, List<Pause> pausesList, List<Employee> assignedEmployees, TaskState currentState, List<Address> addresses, string name)
        {
            Started = started;
            Finished = finished;
            PausesList = pausesList;
            AssignedEmployees = assignedEmployees;
            CurrentState = currentState;
            Addresses = addresses;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public List<Pause> PausesList { get; set; }
        public List<Employee> AssignedEmployees { get; set; }
        public List<Address> Addresses { get; set; }
        public TaskState CurrentState { get; set; }
    }
}