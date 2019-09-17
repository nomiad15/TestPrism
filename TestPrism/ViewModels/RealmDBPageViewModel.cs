using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPrism.Dto;
using TestPrism.Entities;

namespace TestPrism.ViewModels
{
    public class RealmDBPageViewModel : BindableBase
    {
        #region # Local variables

        //Realm realm; 
        // лучше постоянный Instance (с конструктора убирается)
        public Realm _realm => Realm.GetInstance();

        private string _name;
        private string _phone;
        private int _indPol;
        private List<Pol> _listPol;
        private List<Friend> _listFr;
        private Pol _pol;
        private Friend _friend;

        #endregion
        #region # Properties

        public string NameFr { get { return _name; } set { SetProperty(ref _name, value); } }
        public string PhoneFr { get { return _phone; } set { SetProperty(ref _phone, value); } }
        public int IndPol { get { return _indPol; } set { SetProperty(ref _indPol, value); } }
        public List<Pol> ListPol { get { return _listPol; } set { SetProperty(ref _listPol, value); } }
        public List<Friend> ListFr { get { return _listFr; } set { SetProperty(ref _listFr, value); } }
        public Pol Pol { get { return _pol; } set { SetProperty(ref _pol, value); } }
        public Friend Friend { get { return _friend; } set { SetProperty(ref _friend, value); } }

        #endregion
        #region # Commands

        private DelegateCommand<string> _clickCommand;
        private DelegateCommand<Friend> _selFriendCommand;
        public DelegateCommand<string> ClickCommand => _clickCommand ?? (_clickCommand = new DelegateCommand<string>(ExClickCommand));
        public DelegateCommand<Friend> SelectFriendCommand => _selFriendCommand ?? (_selFriendCommand = new DelegateCommand<Friend>(ShowFriendDetails));

        #endregion
        
        #region # RealmDBPageViewModel constructor
        public RealmDBPageViewModel()
        {
            //_realm = Realm.GetInstance();


            //var pol = _realm.Find<Pol>("8e2e716a-539e-40d2-a5c9-e980b5a7d7c4");
            //UpdatePol(pol);
            //var pol = _realm.All<Pol>().Where(_ => _.Name == "Фыв").FirstOrDefault();
            //DeletePol(pol);
            IndPol = -1;
            ListPol = _realm.All<Pol>().ToList();
            ListFr = _realm.All<Friend>().ToList();
            //Get();
        }
        #endregion

        #region # Command CRUD
        private void ExClickCommand(string param)
        {
            switch (param)
            {
                case "Add":
                    Add();
                    break;
                case "Upd":
                    Update(Friend);
                    break;
                case "Del":
                    Delete(Friend);
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region # Command ShowFriendDetails
        private void ShowFriendDetails(Friend friend)
        {
            Friend = friend;
            NameFr = friend.Name;
            PhoneFr = friend.Phone;
            IndPol = ListPol.FindIndex(_ => _.Id == friend.Pol?.Id);
        }
        #endregion

        #region # Get
        public void Get()
        {
            var allFriend = _realm.All<Friend>().ToList();
            var allFriend1 = allFriend.Select(_ => new FriendDto
            {
                Name = _.Name,
                Phone = _.Phone,
                PolStr = _.Pol.Name
            }).ToList();
        }
        #endregion
        #region # Add
        private async void Add()
        {
            var friend = new Friend
            {
                Id = Guid.NewGuid().ToString(),
                Name = NameFr,
                Phone = PhoneFr,
                //Pol = Pol //при синхронном
            };

            //асинхронный
            var polId = Pol?.Id;
            await _realm.WriteAsync(tempRealm =>
            {
                var pol = tempRealm.Find<Pol>(polId);//.All<Pol>().FirstOrDefault(p => p.Id == polId);
                friend.Pol = pol;
                tempRealm.Add(friend);
            });

            //синхронный
            //_realm.Write(() => _realm.Add(friend));

            ListFr = _realm.All<Friend>().ToList();
        }
        #endregion
        #region # Update
        private async void Update(Friend friend)
        {
            //попробовать
            //var friend = (Friend)BindingContext;

            //_realm.Write(() => realm.Add(kristianWithC, update: true));
            //или
            //_realm.Write(() => pol.Name = "Жен");
            if (friend != null)
            {
                var fr = new Friend
                {
                    Id = friend.Id,
                    Name = NameFr,
                    Phone = PhoneFr,
                    //Pol = Pol //при синхронном
                };

                //синхронный
                //_realm.Write(() => _realm.Add(fr, update: true));
                //aсинхронный
                var polId = Pol.Id;
                await _realm.WriteAsync(tempRealm =>
                {
                    var pol = tempRealm.Find<Pol>(polId);
                    fr.Pol = pol;
                    tempRealm.Add(fr, update: true);
                });
            }

            ListFr = _realm.All<Friend>().ToList();
        }
        #endregion
        #region # Delete
        private async void Delete(Friend friend)
        {
            if (friend != null)
            {
                //синхронный
                //_realm.Write(() => _realm.Remove(friend));
                //асинхронный
                var frId = friend.Id;
                await _realm.WriteAsync(tempRealm =>
                {
                    var fr = tempRealm.Find<Friend>(frId);
                    tempRealm.Remove(fr);
                });
            }

            ListFr = _realm.All<Friend>().ToList();
        }
        #endregion
    }
}
