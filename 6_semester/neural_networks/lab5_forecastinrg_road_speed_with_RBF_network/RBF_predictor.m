function [ ] = RBF_predictor( input )

%% prepare training data
training_data = prepare_training_data();

%% calc weights matrix and A matrix (for PCA)
[weight_matrix, A_matrix, r, PCAd_probes] = calc_PCAd_RBF_weight_matrix(training_data);

%% using PCA on input pattern
n = size(weight_matrix,1);

PCAd_input = (input' * A_matrix)';

pre = zeros(n,1);
for i = 1:n
    pre(i) = exp(-1*(distance(PCAd_input, PCAd_probes(:,i))/r)^2);
end

%% get output vector
y = weight_matrix * pre;

%% find and return best match 
recognised_point = find(ismember(y, max(y)))


end

