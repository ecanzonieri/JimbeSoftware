Correct procedure to implement new Task or Sensor:
1) Create the class in JimbeCore/Domain/Entities following the conventions that are already used by other tasks/sensors (Take care to implement the correct interfaces).
2) Create the NHibernate SubMap class in JimbeCore/Domain/Mapping/NHibernate. Use the other SubMap as example.
3) Create the corresponding DataContract in JimbeWCF/DataContracts and take care to add the new "knowntype" in the parent DataContract (for example if you create a new task's descendant you has to modify the task's datacontract).
4) Modify JimbeService/IoC/AutoMapperModule to include the new mapping
5) That's it!
