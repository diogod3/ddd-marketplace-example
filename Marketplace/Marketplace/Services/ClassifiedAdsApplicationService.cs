using System;
using System.Threading.Tasks;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Services;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework.ApplicationServices;
using Marketplace.Framework.EntityStores;

namespace Marketplace.Services
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {
        private readonly IEntityStore _entityStore;
        private readonly ICurrencyLookup _currencyLookup;

        private async Task HandleCreate(Contracts.ClassifiedAds.V1.Create command)
        {
            if (await _entityStore.ExistsAsync<ClassifiedAd>(command.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with id {command.Id} already exists");
            }

            var classifiedAd = new ClassifiedAd(new ClassifiedAdId(command.Id),
                new UserId(command.OwnerId));

            await _entityStore.SaveAsync(command.Id.ToString(), classifiedAd);
        }

        public ClassifiedAdsApplicationService(IEntityStore entityStore, ICurrencyLookup currencyLookup)
        {
            _entityStore = entityStore ?? throw new ArgumentNullException(nameof(entityStore));
            _currencyLookup = currencyLookup ?? throw new ArgumentNullException(nameof(currencyLookup));
        }

        public Task Handle(object command)
            => command switch
            {
                Contracts.ClassifiedAds.V1.Create cmd => HandleCreate(cmd),
                Contracts.ClassifiedAds.V1.SetTitle cmd => HandleUpdate(cmd.Id, t => t.SetTitle(ClassifiedAdTitle.FromString(cmd.Title))),
                Contracts.ClassifiedAds.V1.UpdateText cmd => HandleUpdate(cmd.Id, t => t.UpdateText(ClassifiedAdText.FromString(cmd.Text))),
                Contracts.ClassifiedAds.V1.UpdatePrice cmd => HandleUpdate(cmd.Id, t => t.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup))),
                Contracts.ClassifiedAds.V1.RequestToPublish cmd => HandleUpdate(cmd.Id, t => t.RequestToPublish()),
                _ => throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown")
            };
        private async Task HandleUpdate(Guid classifiedAdId, Action<ClassifiedAd> operation)
        {
            var classifiedAd = await _entityStore.LoadAsync<ClassifiedAd>(classifiedAdId.ToString());

            if (classifiedAd is null)
            {
                throw new InvalidOperationException($"Entity with id {classifiedAdId} cannot be found");
            }

            operation(classifiedAd);

            await _entityStore.SaveAsync(classifiedAdId.ToString(), classifiedAd);
        }
    }
}