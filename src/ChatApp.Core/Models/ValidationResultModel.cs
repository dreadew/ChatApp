namespace ChatApp.Core.Models;

public class ValidationResultModel
{ 
  public bool IsValid { get; set; }
  public List<string> Errors { get; set; } = new List<string>();
}