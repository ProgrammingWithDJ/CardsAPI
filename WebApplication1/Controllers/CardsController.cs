using CardsAPi.Data;
using CardsAPi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        private readonly IUnitOfWork unitOfWork;


        public CardsController(IUnitOfWork unitOfWork)
        {
          
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            //  var cards =await cardsDbContext.Cards.ToListAsync();

            var cards = await unitOfWork.cardRepository.GetCardsAsync();

            var cardDto = from c in cards
                          select new CardDto()
                          {
                              Id = c.Id,
                              CardholderName = c.CardholderName,
                              CardNumber = c.CardNumber,
                              CVV =c.CVC,
                              ExpiryMonth=c.ExpiryMonth,
                              ExpiryYear =c.ExpiryYear

                          };

            return Ok(cardDto);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid Id)
        {
            // var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id==id);
              var card = await unitOfWork.cardRepository.FindCard(Id);

            if(card !=null)
            {
               return Ok(card);
            }

            return NotFound("Card not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();

           await cardsDbContext.Cards.AddAsync(card);

            cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard),new { id = card.Id },card);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {

            var existingCards = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCards != null)
            {
                existingCards.CardholderName = card.CardholderName;
                existingCards.CardNumber = card.CardNumber;
                existingCards.CVC = card.CVC;
                existingCards.ExpiryMonth = card.ExpiryMonth;
                existingCards.ExpiryYear = card.ExpiryYear;
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingCards);

            }

            return NotFound("Card not found");
        }


        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {

            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                cardsDbContext.Cards.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingCard); 
            }

            return NotFound("Card not found");
        }

    }
}
