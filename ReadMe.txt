The unit tests herein illustrate using a DecisionTable (expressed as a generic Dictionary) to control progam flow
in a more maintainable and factorable form than using switch/case or if/else. New requirements can be handled
simply by appending the appropriate entries to the DecisionTable. 

The preferred mechanisms for initializing the DecisionTable are the static class method and the IOC method,
both illustrated in unit tests. Choice between methods should be determined by runtime extensibility needs.

Also shown is the use of an extension method to add validation logic to a set accessor.