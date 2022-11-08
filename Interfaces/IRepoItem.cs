namespace TheKeep.Interfaces;

public interface IRepoItem<T>
{
  // REVIEW here 'T' becomes the placeholder for the type we want to return.... interfaces say this is what the data 'looks like'
  T Id { get; set; }
}