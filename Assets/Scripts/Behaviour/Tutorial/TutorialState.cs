public abstract class TutorialState {
	public virtual void OnStart()  {}
	public virtual bool Update()   => true;
	public virtual void OnFinish() {}
}