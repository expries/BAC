using LwwScenarios = BAC.Scenarios.LastWriteWinsCRDT;
using LwwWithClockSkewScenarios = BAC.Scenarios.LastWriteWinsCRDTWithClockSkew;
using PutWinsScenarios = BAC.Scenarios.PutWinsCRDT;
using MergingScenarios = BAC.Scenarios.MergingValuesCRDT;

// Scenario 0
LwwScenarios.Scenario0.Show();
LwwWithClockSkewScenarios.Scenario0.Show();
PutWinsScenarios.Scenario0.Show();
MergingScenarios.Scenario0.Show();

// Scenario 1
LwwScenarios.Scenario1.Show();
LwwWithClockSkewScenarios.Scenario1.Show();
PutWinsScenarios.Scenario1.Show();
MergingScenarios.Scenario1.Show();

// Scenario 2
LwwScenarios.Scenario2.Show();
LwwWithClockSkewScenarios.Scenario2.Show();
PutWinsScenarios.Scenario2.Show();
MergingScenarios.Scenario2.Show();

// Scenario 3
LwwScenarios.Scenario3.Show();
LwwWithClockSkewScenarios.Scenario3.Show();
PutWinsScenarios.Scenario3.Show();
MergingScenarios.Scenario3.Show();
