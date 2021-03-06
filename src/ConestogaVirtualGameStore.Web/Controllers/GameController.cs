﻿namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository;

    public class GameController : Controller
    {
        private readonly IGameRepository gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public IActionResult Index()
        {
            return View(this.gameRepository.GetGames());
        }
        
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Price,Date,Developer,Publisher,RecordId")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.ImageFileName = string.Empty;
                this.gameRepository.AddGame(game);
                this.gameRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Game/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Title,Description,Price,Date,Developer,Publisher,RecordId")] Game game)
        {
            if (id != game.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    game.ImageFileName = String.Empty;
                    this.gameRepository.UpdateGame(game);
                    this.gameRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.RecordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Game/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var game = this.gameRepository.GetGame(id);

            this.gameRepository.RemoveGame(game);
            this.gameRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(long id)
        {
            return this.gameRepository.Exists(id);
        }
    }
}